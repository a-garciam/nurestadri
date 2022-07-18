using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.ModelUtil.Transactions;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output;
using Es.Udc.DotNet.PracticaMaD.Model.Services.Exceptions;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService
{
    public class CommentService : ICommentService
    {
        [Inject]
        public ICommentDao CommentDao { private get; set; }

        [Inject]
        public IImageDao ImageDao { private get; set; }

        [Inject]
        public IUserDao UserDao { private get; set; }

        // Añadir comentario.
        // Un usuario puede añadir comentarios relativos a una imagen.
        // Si el usuario no estaba autenticado, cuando selecciona el enlace
        // para añadir un comentario, se le redirige al formulario de autenticación
        // y tras autenticarse correctamente, se le muestra el formulario para añadir comentario.
        // Un usuario también podrá modificar o eliminar los comentarios realizados por él mismo.

        /// <summary>
        /// Adds a comment to an image.
        /// </summary>
        /// <param name="userId"> The user id. </param>
        /// <param name="imageId"> The image id. </param>
        /// <param name="text"> The text of the comment. </param>

        [Transactional]
        public long CommentImage(long userId, long imageId, String text)
        {
            User user = UserDao.Find(userId);
            Image image = ImageDao.Find(imageId);
            string textComment = text.Trim();
            if (textComment.Length <= 0)
            {
                throw new EmptyCommentException("Comment must not be empty");
            }
            if (textComment.Length > 200)
            {
                throw new ExceededLengthException();
            }
            Comment comment = new Comment()
            {
                text = textComment,
                creationDate = DateTime.Now,
                Image = image,
                User = user
            };

            CommentDao.Create(comment);
            return comment.commentId;
        }

        /// <summary>
        /// Displays all the comments of an image.
        /// </summary>
        /// <param name="imageId"> The image id. </param>
        [Transactional]
        public CommentBlock FindCommentsByImage(long imageId, int startIndex, int count)
        {
            ImageDao.Find(imageId);
            IList<Comment> comments = CommentDao.FindByImageId(imageId, startIndex, count + 1);
            bool existMore = (comments.Count == count + 1);

            if (existMore)
                comments.RemoveAt(count);

            IList<CommentOutput> commentList = new List<CommentOutput>();
            User user = new User();
            foreach (Comment comment in comments)
            {
                user = UserDao.Find(comment.usrId);
                commentList.Add(new CommentOutput(
                    comment.commentId,
                    user.loginName,
                    comment.usrId,
                    comment.creationDate,
                    comment.text
                    ));
            }

            return new CommentBlock(commentList, existMore);
        }

        // Indicar “Me gusta”.
        // Un usuario puede indicar que le gusta una imagen.
        // Si el usuario no estaba autenticado, cuando selecciona el enlace
        // para añadir un comentario, se le redirige al formulario de autenticación
        // y tras autenticarse correctamente, se le muestra el formulario para añadir
        // su “Me gusta”.
        // No será posible indicar “Me gusta” más de una vez sobre la misma imagen,
        // pero sí podrá eliminarse un “Me gusta” especificado previamente.

        /// <summary>
        /// Adds (or deletes if already exists) a like to an image.
        /// </summary>
        /// <param name="imageId"> The image id. </param>
        /// <param name="userId"> The user id. </param>
        [Transactional]
        public int LikeImage(long imageId, long userId)
        {
            Image image = ImageDao.Find(imageId);
            User user = UserDao.Find(userId);

            if (!image.UserLikes.Contains(user))
            {
                image.likes++;
                image.UserLikes.Add(user);
                user.ImageLikes.Add(image);
                ImageDao.Update(image);
                UserDao.Update(user);
                return image.likes;
            }
            else
            {
                if (image.likes >= 0)
                {
                    image.likes--;
                    image.UserLikes.Remove(user);
                    user.ImageLikes.Remove(image);
                    ImageDao.Update(image);
                    UserDao.Update(user);
                }
                return image.likes;
            }
        }

        [Transactional]
        public bool FindLike(long imageId, long userId)
        {
            Image image = ImageDao.Find(imageId);
            User user = UserDao.Find(userId);
            if (image.UserLikes.Contains(user))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Transactional]
        public void UpdateComment(long userId, long commentId, string text)
        {
            User user = UserDao.Find(userId);

            Comment comment = CommentDao.Find(commentId);

            if (user.usrId != comment.usrId)
            {
                throw new OperationNotAllowedException();
            }
            if (text.Length > 200)
            {
                throw new ExceededLengthException();
            }
            comment.text = text;

            CommentDao.Update(comment);
        }

        [Transactional]
        public CommentOutput FindCommentsById(long commentId)
        {
            Comment comment = CommentDao.Find(commentId);
            User user = UserDao.Find(comment.usrId);

            return new CommentOutput(
                    comment.commentId,
                    user.loginName,
                    comment.usrId,
                    comment.creationDate,
                    comment.text
                    );
        }

        [Transactional]
        public void DeleteComment(long userId, long commentId)
        {
            Comment comment = CommentDao.Find(commentId);

            if (userId != comment.usrId)
            {
                throw new OperationNotAllowedException();
            }
            CommentDao.Remove(comment.commentId);
        }
    }
}