namespace ComMethods
{
    public interface IIteratorSolver
    {
        double Eps { set; get; }
        int Iter { set; get; }
        int MaxIter { set; get; }
    }
}