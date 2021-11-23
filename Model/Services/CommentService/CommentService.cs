using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Udc.DotNet.PracticaMaD.Model.Daos.CommentDao;
using Ninject;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService
{
    public class CommentService : ICommentService
    {

        [Inject]
        public ICommentDao CommentDao { private get; set; }


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

        public long CommentImage(User user, Image image, String text)
        {
            Comment comment = new Comment();
            comment.User = user;
            comment.Image = image;
            comment.text = text;
            CommentDao.Create(comment);

            return comment.commentId;
        }

        /// <summary>
        /// Deletes a comment from an image.
        /// </summary>
        /// <param name="commentId"> The comment id. </param>

        public long DeleteComment(int commentId)
        {
            Comment comment = CommentDao.Find(commentId);
            if (comment != null)
            {
                CommentDao.Remove(comment.commentId);
            }

            return comment.commentId;
        }

        /// <summary>
        /// Edits a comment from an image.
        /// </summary>
        /// <param name="commentId"> The comment id. </param>
        /// <param name="newText"> The new text of the comment. </param>

        public long EditComment(Comment comm, String newText)
        {
            Comment comment = CommentDao.Find(comm.commentId);
            comment.text = newText;
            CommentDao.Update(comment);
            return comment.commentId;
        
        }

        /// <summary>
        /// Displays all the comments of an image.
        /// </summary>
        /// <param name="imageId"> The image id. </param>

        public void DisplayComments(Image image)
        {
            List<Comment> comments = CommentDao.FindByImageId(image.imageId);
            for(int i = 0; comments[i] != null; i++)
            {
                string str = comments[i].ToString();
                Console.WriteLine(str);
            }

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

        //public long LikeImage(Image image, User user)
        //{
        //    Like like = new Like();
        //    like.Image = image;
        //    like.User = user;
        //    if (LikeDao.Exists(like.likeId))
        //    {
        //        LikeDao.Remove(like.likeId);
        //    }
        //    else {
        //        LikeDao.Create(like);
        //    }
        //    return like.likeId;
        //}


    }
}
