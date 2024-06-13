using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seminar5TEST
{
    public class MasiniPompieri
    {
        private string numeMasina;
        private int dataFabricare;
        private float greutate;

        public MasiniPompieri()
        {

        }

        public MasiniPompieri(string numeMasina, int data, float greutate)
        {
            this.numeMasina = numeMasina;
            this.dataFabricare = data;
            this.greutate = greutate;
        }

        public string NumeMasina { get => numeMasina; set => numeMasina = value; }
        public int DataFabricare { get => dataFabricare; set => dataFabricare = value; }
        public float Greutate {  get => greutate; set => greutate = value; }    
    }
}
