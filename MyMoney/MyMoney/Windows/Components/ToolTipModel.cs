using System.Windows.Forms;

namespace MyMoney.Windows.Components
{
    public class ToolTipModel
    {
        public readonly Control Anchor;

        public readonly string Title;

        public readonly string Message;

        public ToolTipModel(string title, string message, Control anchor)
        {
            Anchor = anchor;
            Title = title;
            Message = message;
        }

        public bool Equals(ToolTipModel toCheck)
        {
            var sameTitle = Title.Equals(toCheck.Title);
            var sameMessage = Message.Equals(toCheck.Message);

            return SameAnchor(toCheck) && sameTitle && sameMessage;
        }

        public bool SameAnchor(ToolTipModel toCheck)
        {
            return Anchor.Equals(toCheck.Anchor);
        }
    }
}
