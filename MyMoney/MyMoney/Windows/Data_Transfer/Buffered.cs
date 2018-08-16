using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoney.Windows.Data_Transfer
{
    /// <summary>
    /// A window that may recive a <see cref="Packet"/>. Buffered windows 
    /// must have a <see cref="System.ComponentModel.BackgroundWorker"/> for 
    /// transfering files otherwise they are not buffered.
    /// </summary>
    public interface Buffered
    {
        /// <summary>
        /// Take a paket and parse it for use in the window.
        /// </summary>
        /// <param name="data"></param>
        void recieve(Packet data);

    }
}
