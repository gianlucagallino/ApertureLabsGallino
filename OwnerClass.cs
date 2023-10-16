using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureLabsGallino
{
    internal class Owner
    {
        public long dni;
        public bool isVip;

        public Owner()
        {
            Random randDNI = new Random();
            this.dni = randDNI.Next(1, 99999999); //Genera un dni entre 1 y 8 digitos
            Random randVip= new Random();
            if (randVip.Next(1, 2) % 2 == 0) //genera un trueFalse
            {
                this.isVip = true;
            }
            else
            {
                this.isVip = false;
            }
        }
    }
}
