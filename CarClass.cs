using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureLabsGallino
{
    internal class Car
    {
        string model;
        public Owner carOwner;
        string licensePlate;
        double width;
        double length;
        short sizeType; // 1=mini, 2=standard, 3=max

        public void determineSize(double wdth, double lgth)
        {
            if (wdth>5 || lgth>2) {
                this.sizeType = 3;
            }
            else if (wdth < 1.5 && lgth < 4)
            {
                this.sizeType = 1;
            }
            else
            {
                this.sizeType = 2;
            }
        }

        public Car()
        {
            Random modelToSet = new Random();
            Random licensePlateRNG = new Random();
            Random widthRNG = new Random();
            Random lengthRNG = new Random();
            long randNum = modelToSet.Next(1, (Enum.GetNames(typeof(CarModel))).Length);
            string modelName = (string)Enum.GetNames(typeof(CarModel)).GetValue(randNum);
            this.model = modelName;
            this.carOwner = new Owner();
            this.licensePlate = "apert"+licensePlateRNG.Next(0, 999999999); //genera una plate
            this.width = widthRNG.NextDouble()*3.5;//genera una width entre 0.35 y 3.5
            this.length = lengthRNG.NextDouble() * 7;//genera una length entre 0.7 y 7
            determineSize(this.width, this.length);
        }

        public void Show()
        {
            Console.WriteLine($"\n Car model: {this.model}");
            this.carOwner.Show();
            Console.WriteLine($"License plate: {this.licensePlate}");
            if (this.sizeType == 1) {
                Console.WriteLine("Size: Mini");
            }
            else if (this.sizeType == 2)
            {
                Console.WriteLine("Size: Standard");
            }
            else
            {
                Console.WriteLine("Size: Max");
            }
        }
        public bool CheckVip()
        {
            if (this.carOwner.isVip == true) return true;
            else return false;
        }
    }
}
