using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Servicios
{

    class ServicioAzure
    {
        private readonly string claveConexion = Properties.Settings.Default.ClaveAzure;

        private readonly string nombreContenedorImagenesAzure = Properties.Settings.Default.NombreContenedorImagenesAzure;
        private readonly string nombreContenedorArticulosAzure = Properties.Settings.Default.NombreContenedorArticulosAzure;

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
