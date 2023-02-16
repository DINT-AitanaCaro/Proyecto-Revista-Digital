using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{
    /// <summary>
    ///     Servicio para gestionar operaciones con Azure.
    /// </summary>
    class ServicioAzure
    {
        /// <summary>
        ///     Clave para acceder a los servicios de Azure.
        /// </summary>
        private readonly string claveConexion = Properties.Settings.Default.ClaveAzure;
        /// <summary>
        ///     Nombre del contenedor de las imágenes en Azure.
        /// </summary>
        private readonly string nombreContenedorImagenesAzure = Properties.Settings.Default.NombreContenedorImagenesAzure;
        /// <summary>
        ///     Nombre del contenedor de los artículos en Azure.
        /// </summary>
        private readonly string nombreContenedorArticulosAzure = Properties.Settings.Default.NombreContenedorArticulosAzure;

        /// <summary>
        ///     Método para almacenar una imagen en el BlobStorage de Azure.
        /// </summary>
        /// <param name="rutaImagen">string que contiene la ruta de la imagen en el sistema.</param>
        /// <returns>Ruta que Azure le ha asignado a la imagen.</returns>
        public string AlmacenarImagenEnLaNube(string rutaImagen)
        {
            if (rutaImagen != "")
            {
                var clienteBlobService = new BlobServiceClient(claveConexion);
                var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorImagenesAzure);

                Stream streamImagen = File.OpenRead(rutaImagen);
                string nombreImagen = Path.GetFileName(rutaImagen);

                if (!clienteContenedor.GetBlobClient(nombreImagen).Exists())
                    clienteContenedor.UploadBlob(nombreImagen, streamImagen);

                var clienteBlobImagen = clienteContenedor.GetBlobClient(nombreImagen);
               
                return clienteBlobImagen.Uri.AbsoluteUri;
            }
            else return "";

        }

        /// <summary>
        ///     Método para subir un pdf a Azure.
        /// </summary>
        /// <param name="rutaPDF">string que contiene la ruta del pdf en el sistema.</param>
        /// <returns>Ruta del pdf en Azure.</returns>
        public string AlmacenarPDFEnLaNube(string rutaPDF)
        {
            var clienteBlobService = new BlobServiceClient(claveConexion);
            var clienteContenedor = clienteBlobService.GetBlobContainerClient(nombreContenedorArticulosAzure);

            Stream pdfArticulo = File.OpenRead(rutaPDF);
            string nombrePDF = Path.GetFileName(rutaPDF);

            if (!clienteContenedor.GetBlobClient(nombrePDF).Exists())
                clienteContenedor.UploadBlob(nombrePDF, pdfArticulo);

            var clienteBlobPDF = clienteContenedor.GetBlobClient(nombrePDF);

            return clienteBlobPDF.Uri.AbsoluteUri;
        }
    }
}
