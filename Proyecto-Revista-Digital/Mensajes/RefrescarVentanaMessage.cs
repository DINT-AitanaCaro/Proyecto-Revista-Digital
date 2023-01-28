using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Revista_Digital.Mensajes
{
    class RefrescarVentanaMessage : ValueChangedMessage<bool>
    {
        public RefrescarVentanaMessage(bool value) : base(value) { }
    }
}
