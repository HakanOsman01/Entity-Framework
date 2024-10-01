namespace Military_Elite.Contracts
{
    public interface ILieutenantGeneral : IPrivate
    {
        public List<IPrivate> Privates { get; }

    }
}
