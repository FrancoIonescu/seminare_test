using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Seminar5TEST
{

    [Serializable]
    public class Pompier : MasiniPompieri, ICloneable, IComparable<Pompier>
    {
        private string nume;
        private int varsta;
        private float gpa;
        private DateTime dataAngajare;

        public Pompier()
        {

        }

        public Pompier(string nume, int varsta, float gpa, DateTime dataAngajare)
        {
            this.nume = nume;
            this.varsta = varsta;
            this.gpa = gpa;
            this.dataAngajare = dataAngajare;
        }

        public string Nume { get => nume; set => nume = value; }
        public int Varsta { get => varsta; set => varsta = value; }
        public float Gpa { get => gpa; set => gpa = value; }
        public DateTime DataAngajare { get => dataAngajare; set => dataAngajare = value; }

        public object Clone()
        {
            Pompier copie = (Pompier)this.MemberwiseClone();
            return copie;
        }

        public int CompareTo(Pompier p)
        {
            return this.Varsta.CompareTo(p.Varsta);
        }
        public static bool operator < (Pompier p1, Pompier p2)
        {
            return p1.varsta < p2.varsta;
        } 

        public static bool operator > (Pompier p1, Pompier p2)
        {
            return p1.varsta > p2.varsta;
        }

        public static int operator + (Pompier p, int varsta )
        {
            return p.varsta + varsta;
        }

        public override string ToString()
        {
            return nume + ", " + varsta + ", " + gpa + ", " + dataAngajare;
        }
    }
}
