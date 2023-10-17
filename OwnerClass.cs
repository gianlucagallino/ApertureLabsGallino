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
            Random randVip = new Random();
            if (randVip.Next(1, 10) % 2 == 0) //genera un trueFalse
            {
                this.isVip = true;
            }
            else
            {
                this.isVip = false;
            }
        }
        public void Show()
        {
            Console.WriteLine($"Owner DNI: {this.dni}");
            if (this.isVip == true)
            {
                Console.WriteLine("VIP: Yes");
            }
            else
            {
                Console.WriteLine("VIP: No");
            }
        }

        public void load()
        {
            Console.WriteLine("Enter DNI");
            long dniValue = Convert.ToInt64(Console.In.ReadLine());
            this.dni = dniValue;
            Console.WriteLine("Is the owner a VIP? 1 for yes, 0 for no.");
            long vipValue = Convert.ToInt64(Console.In.ReadLine());
            if (vipValue == 1)
            {
                this.isVip = true;
            }
            else this.isVip = false;
        }
    }
}
