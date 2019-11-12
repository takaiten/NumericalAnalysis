using System.ComponentModel.Design;

namespace ComMethods.Solvers
{
    public class BiSmth
    {
        public int MaxIter { set; get; }
        public int Iter { set; get; }
        public double Eps { set; get; }

        public Preconditioner Preconditioner;
        // TODO: this
    }
}