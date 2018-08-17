using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;
using System.Windows.Forms;

namespace MyMoney.Windows.Data_Transfer
{
    /// <summary>
    /// Used for transfering objects between <see cref="Buffered"/> windows.
    /// </summary>
    public class Packet
    {
        /// <summary>
        /// The <see cref="Buffered"/> window this <see cref="Packet"/> originated from.
        /// </summary>
        public Buffered source;

        /// <summary>
        /// The destination <see cref="Buffered"/> window of this <see cref="Packet"/>.
        /// </summary>
        public Buffered destination;

        /// <summary>
        /// The contents of the <see cref="Packet"/>. Each <see cref="destination"/> will 
        /// parse this object differently depending on the <see cref="source"/>.
        /// </summary>
        public Object data;

        /// <summary>
        /// Creates a new <see cref="Packet"/>
        /// </summary>
        /// <param name="source">
        /// The <see cref="Buffered"/> window this <see cref="Packet"/> originated from.
        /// </param>
        /// <param name="destination">
        /// The destination <see cref="Buffered"/> window of this <see cref="Packet"/>.
        /// </param>
        /// <param name="data">
        /// The contents of the <see cref="Packet"/>.
        /// </param>
        public Packet(Buffered source, Buffered destination, Object data)
        {
            this.source = source;
            this.data = data;
            this.destination = destination;
        }


    }

    
}
