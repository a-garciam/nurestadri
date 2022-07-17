using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.Services.CommentService
{
    public interface ICommentService
    {
        // Añadir comentario.
        // Un usuario puede añadir comentarios relativos a una imagen.
        // Si el usuario no estaba autenticado, cuando selecciona el enlace
        // para añadir un comentario, se le redirige al formulario de autenticación
        // y tras autenticarse correctamente, se le muestra el formulario para añadir comentario.
        // Un usuario también podrá modificar o eliminar los comentarios realizados por él mismo.

        long CommentImage(long userId, long imageId, String text);

        long DeleteComment(int commentId);

        long EditComment(long commentId, String newText);

        void DisplayComments(long imageId);

        // Indicar “Me gusta”.
        // Un usuario puede indicar que le gusta una imagen.
        // Si el usuario no estaba autenticado, cuando selecciona el enlace
        // para añadir un comentario, se le redirige al formulario de autenticación
        // y tras autenticarse correctamente, se le muestra el formulario para añadir
        // su “Me gusta”.
        // No será posible indicar “Me gusta” más de una vez sobre la misma imagen,
        // pero sí podrá eliminarse un “Me gusta” especificado previamente.

        int LikeImage(long imageId, long userId); // Si se llama a la función y el Like ya existe, se elimina. Devuelve el numero de likes de la foto tras la operacion

        bool FindLike(long imageId, long userId);
    }
}