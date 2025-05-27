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
            _model.OnStateChanged += Model_OnStateChanged;
            _model.OnResRemoved += Model_OnResRemoved;
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
        }

        private void Model_OnStateChanged(string obj)
        {
            throw new System.NotImplementedException();
        }

        private void Model_OnResRemoved(string obj)
        {
            if (obj != _resId)
                return;

            Dispose();
        }

        private void Dispose()
        {
            _model.OnAmountChanged -= Model_OnAmountChanged;
            _model.OnStateChanged -= Model_OnStateChanged;
            _model.OnResRemoved -= Model_OnResRemoved;

            _view.Destroy();
        }
    }
}
