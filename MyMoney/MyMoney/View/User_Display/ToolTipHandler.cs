using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace MyMoney.Windows.User_Display
{
    /// <summary>
    /// Encapsultes all the behaviours of a toolTip. This hadler only 
    /// allows one toolTip to be visible to the user at any given time.
    /// </summary>
    public class ToolTipHandler
    {
        /// <summary>
        /// The tool tip that will be displayed to the user.
        /// </summary>
        private ToolTip toolTip;

        /// <summary>
        /// The current control that the tool tip is displayed over.
        /// </summary>
        private Control control;

        /// <summary>
        /// The title of the current tool tip.
        /// </summary>
        private string title;

        /// <summary>
        /// The message of the current tool tip.
        /// </summary>
        private string message;

        /// <summary>
        /// Constructs a new tool tip handler.
        /// </summary>
        public ToolTipHandler()
        {
            this.toolTip = new ToolTip(); ;
            this.control = null;
            this.title = "";
            this.message = "";
        }

        /// <summary>
        /// Draws a new tool tip over a specified control. If this control
        /// is the same as the current <see cref="control"/> then a new tool
        /// tip will not be displayed.
        /// </summary>
        /// <param name="newTitle">Title of the new tool tip.</param>
        /// <param name="newMessage">Message of the new tool tip.</param>
        /// <param name="newControl">The control the new tool tip will be displayed over.</param>
        public void draw(string newTitle, string newMessage, Control newControl)
        {
            // Holds whether the new conrol is the smae as the current control.
            bool sameControl = newControl.Equals(control);

            // Holds whether the new title of the tool tip is the same as the current one.
            bool sameTitle = this.title.Equals(newTitle);

            // Holds whether the new title of the tool tip is the same as the current one.
            bool sameMessage = this.message.Equals(newMessage);

            // If the control, title and message aren't the same.
            if (!(sameControl && sameTitle && sameMessage))
            {
                //Hide the current tool tip
                if (control != null) toolTip.Hide(control);

                // Create a new tool tip with the new tite.
                ToolTip newToolTip = new ToolTip();
                newToolTip.ToolTipTitle = newTitle;

                // Re-assign the fields to their new counterparts
                this.toolTip = newToolTip;
                this.control = newControl;
                this.title = newTitle;
                this.message = newMessage;

                // Show the tool tip
                toolTip.Show(newMessage, control);

            }
            // If the control is the same but it has faded away.
            else if (sameControl && !string.IsNullOrEmpty(toolTip.GetToolTip(control)))
            {
                // Show the tool tip.
                toolTip.Show(newMessage, control);
            }
        }

    }

}
