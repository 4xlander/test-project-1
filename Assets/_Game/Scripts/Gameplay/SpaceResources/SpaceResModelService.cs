using System.Collections.Generic;

namespace Game
{
    public class SpaceResModelService
    {
        private readonly List<SpaceResModel> _models;

        public SpaceResModelService()
        {
            _models = new List<SpaceResModel>();
        }

        public IReadOnlyList<SpaceResModel> Models => _models;

        public SpaceResModel CreateModel(SpaceResConfig config)
        {
            var newData = new SpaceResData
            {
                Type = config.Resource,
                Amount = config.Amount,
            };
            var newModel = new SpaceResModel(newData);
            _models.Add(newModel);

            return newModel;
        }

        public void RemoveModel(SpaceResModel model)
        {
            if (_models.Contains(model))
                _models.Remove(model);
        }
    }
}
