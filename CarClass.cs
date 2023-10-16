using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApertureLabsGallino
{
    internal class Car
    {
        string model;
        Owner carOwner;
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

        Car()
        {
            Random modelToSet = new Random();
            Random licensePlateRNG = new Random();
            Random widthRNG = new Random();
            Random lengthRNG = new Random();
            long randNum = modelToSet.Next(1, (Enum.GetNames(typeof(CarModel))).Length);
            string modelName = (string)Enum.GetNames(typeof(CarModel)).GetValue(randNum);
            this.model = modelName;
            this.carOwner = new Owner();
            this.licensePlate = "apert"+licensePlateRNG.Next(0, 999); //genera una plate
            this.width = widthRNG.NextDouble()*3.5;//genera una width entre 0.35 y 3.5
            this.length = lengthRNG.NextDouble() * 7;//genera una length entre 0.7 y 7
            determineSize(this.width, this.length);
        }
    }
}
