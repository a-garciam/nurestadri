﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.Daos;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.UserDao;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources;
using Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService.Resources.Output;
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

        public long CommentImage(long userId, long imageId, String text)
        {
            if (UserDao.Exists(userId) && ImageDao.Exists(imageId))
            {
                Comment comment = new Comment();
                Image image = ImageDao.Find(imageId);
                User user = UserDao.Find(userId);
                comment.User = user;
                comment.Image = image;
                comment.text = text;
                CommentDao.Create(comment);
                image.Comments.Add(comment);
                user.Comments.Add(comment);

                return image.Comments.Count;
            }
            else
                return -1;
        }

        /// <summary>
        /// Deletes a comment from an image.
        /// </summary>
        /// <param name="commentId"> The comment id. </param>

        public long DeleteComment(int commentId)
        {
            if (CommentDao.Exists(commentId))
            {
                Comment comment = CommentDao.Find(commentId);
                User user = UserDao.Find(comment.usrId);
                Image image = ImageDao.Find(comment.usrId);
                CommentDao.Remove(commentId);
                user.Comments.Remove(comment);
                image.Comments.Remove(comment);

                return comment.commentId;
            }
            else return -1;
        }

        /// <summary>
        /// Edits a comment from an image.
        /// </summary>
        /// <param name="commId"> The comment id. </param>
        /// <param name="newText"> The new text of the comment. </param>

        public long EditComment(long commId, String newText)
        {
            if (CommentDao.Exists(commId))
            {
                Comment comment = CommentDao.Find(commId);
                comment.text = newText;
                CommentDao.Update(comment);
                return comment.commentId;
            }
            else return -1;
        }

        /// <summary>
        /// Displays all the comments of an image.
        /// </summary>
        /// <param name="imageId"> The image id. </param>

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

        public int LikeImage(long imageId, long userId)
        {
            Image image = ImageDao.Find(imageId);
            User user = UserDao.Find(userId);

            if (image.UserLikes.Contains(user))
            {
                image.likes--;
                image.UserLikes.Remove(user);
                user.ImageLikes.Remove(image);
                ImageDao.Update(image);
                UserDao.Update(user);
                return image.likes;
            }
            else
            {
                image.likes++;
                image.UserLikes.Add(user);
                user.ImageLikes.Add(image);
                return image.likes;
            }
        }

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
    }
}