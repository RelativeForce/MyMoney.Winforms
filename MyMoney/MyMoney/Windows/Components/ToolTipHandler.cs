using System;
using System.Windows.Forms;

namespace MyMoney.Windows.Components
{

    public class ToolTipHandler
    {
        private ToolTip _toolTip;

        private ToolTipModel _model;

        public ToolTipHandler()
        {
            _toolTip = null;
            _model = null;
        }

        public void Draw(ToolTipModel model)
        {

            if (model == null) throw new Exception("ToolTip cannot be null");

            if (_model == null) Show(model);

            var same = _model.Equals(model);

            if (same) return;

            var sameAnchor = _model.SameAnchor(model);

            if (sameAnchor)
            {
                AlterVisible(model);
            }
            else
            {
                Swap(model);
            }
        }

        private void Swap(ToolTipModel model)
        {
            _toolTip.Hide(_model.Anchor);

            Show(model);
        }

        private void Show(ToolTipModel model)
        {
            _model = model;

            var newToolTip = new ToolTip { ToolTipTitle = _model.Title };
            newToolTip.Show(_model.Message, _model.Anchor);

            _toolTip = newToolTip;

        }

        private void AlterVisible(ToolTipModel model)
        {
            _model = model;
            _toolTip.Show(_model.Message, _model.Anchor);
        }

    }

}
