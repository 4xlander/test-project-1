namespace Game
{
    public class SpaceResController
    {
        private readonly string _resId;
        private readonly SpaceResModel _model;
        private readonly SpaceResView _view;

        public SpaceResController(
            string resId, SpaceResModel model, SpaceResView view)
        {
            _resId = resId;
            _model = model;
            _view = view;

            _model.OnAmountChanged += Model_OnAmountChanged;
        }

        public void Init(SpaceResConfig config)
        {
            _view.UpdateAmount(_model.GetAmount(_resId));
        }

        private void Model_OnAmountChanged(string resId)
        {
            if (_resId != resId)
                return;

            var amount = _model.GetAmount(_resId);
            _view.UpdateAmount(amount);

            if (amount <= 0)
            {
                _model.OnAmountChanged -= Model_OnAmountChanged;
                _model.RemoveRes(_resId);
                _view.Destroy();
            }
        }
    }
}
