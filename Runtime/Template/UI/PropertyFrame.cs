namespace ProjectTemplate
{
    public abstract partial class PropertyFrame<TProperty> : UIElement where TProperty : Property
    {
        #region Public Methods

        public virtual partial void Initialize(TProperty property, Setting setting)
        {
            void ChangeText() => _title.text = property.Name[Language];
            LanguagesManager.Instance.BindAndApply(ChangeText);

            _bindedSetting = setting;
        }

        #endregion
    }
}