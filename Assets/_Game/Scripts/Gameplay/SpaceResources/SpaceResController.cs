namespace Game
{
    public class SpaceResController
    {
        private readonly SpaceResModelService _modelService;

        private SpaceResModel _model;
        private SpaceResView _view;

        public SpaceResController(SpaceResModelService modelService)
        {
            _modelService = modelService;
        }

        public void Init(SpaceResConfig resourceConfig, SpaceResView resourceView)
        {
            _view = resourceView;

            _model = _modelService.CreateModel(resourceConfig);
            _model.SetPosition(_view.transform.position);
            _model.OnAmountChanged += Model_OnAmountChanged;

            _view.AmountText.text = _model.Amount.ToString();
        }

        private void Model_OnAmountChanged()
        {
            _view.AmountText.text = _model.Amount.ToString();

            if (_model.Amount <= 0)
            {
                _model.OnAmountChanged -= Model_OnAmountChanged;

                _modelService.RemoveModel(_model);
                _model = null;

                _view.Destroy();
                _view = null;
            }
        }
    }
}
