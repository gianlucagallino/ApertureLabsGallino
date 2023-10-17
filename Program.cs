using System.ComponentModel.Design;

namespace ApertureLabsGallino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Gallino Gianluca

            bool isRunning = true;
            string menuPick;
            Car[] NormalParking = new Car[12];
            List<Car> QuantumParking = new List<Car>();


            LoadRandomData(NormalParking, QuantumParking);

            while (isRunning)
            {

                Console.Clear();
                Console.Out.WriteLine("\n             Welcome to the Aperture Science              ");
                Console.Out.WriteLine(" Parking Entry Non-symmetric Infinite System! (P.E.N.I.S).\n");
                Console.Out.WriteLine("                     Management Menu                      ");
                Console.Out.WriteLine(" ---------------------------------------------------------");
                Console.Out.WriteLine("                  1. List all vehicles                    ");
                Console.Out.WriteLine("                   2. Add new vehicle                     ");
                Console.Out.WriteLine("            3. Remove vehicle by License plate            ");
                Console.Out.WriteLine("              4. Remove vehicle by owner DNI              ");
                Console.Out.WriteLine("            5. Remove random amount of vehicles           ");
                Console.Out.WriteLine("                6. Optimise parking spaces                  ");
                Console.Out.WriteLine(" ---------------------------------------------------------");
                Console.Out.WriteLine("                  Please enter your Pick:                        ");
                menuPick = Console.In.ReadLine();

                switch (menuPick)
                {
                    case "1":
                        ListAllVehicles();
                        break;
                    case "2":
                        AddNewVehicle();
                        break;
                    case "3":
                        RemoveVehicleByLP();
                        break;
                    case "4":
                        RemoveVehicleByDNI();
                        break;
                    case "5":
                        //RemoveRandomAmount();
                        break;
                    case "6":
                        //OptimiseParkingSpaces();
                        break;
                    default:
                        Console.Clear();
                        Console.Out.WriteLine(" Pay attention to your inputs, please. (Press any key) ");
                        Console.In.ReadLine(); //No guarda el input, es una pausa.
                        break;
                }
            }


            void LoadRandomData(Car[] Normal, List<Car> Quantum)
            {
                for (int i = 0; i < Normal.Length; i++) //Esto llena el estacionamiento normal, para poder llegar a usar el quantum 
                {
                    Normal[i] = new Car();
                    if ((i == 3 || i == 7 || i == 12) && (Normal[i].CheckVip() == false))
                    {
                        while (Normal[i].CheckVip() == false)
                        {
                            Quantum.Add(Normal[i]); //si no es vip, lo patea a la n dimension. no se que tan buena idea es pero funciona
                            Normal[i] = new Car();
                        }
                    }
                }
                Random QuantumCarAmount = new Random();
                for (int i = 0; i < QuantumCarAmount.Next(1, 100); i++)
                {
                    Quantum.Add(new Car());
                }
            }

            void ListAllVehicles()
            {
                foreach (Car Vehicle in NormalParking)
                {
                    if (Vehicle.model != "Deleted")
                    {
                        Vehicle.Show();
                    }
                }
                foreach (Car Vehicle in QuantumParking)
                {
                    if (Vehicle.model != "Deleted")
                    {
                        Vehicle.Show();
                    }
                }
                Console.Out.WriteLine("(Press any key) ");
                Console.In.ReadLine(); //No guarda el input, es una pausa.
            }

            void AddNewVehicle()
            {

                Console.Out.WriteLine("Press 1 to load vehicle values, Press 2 to write random values. ");
                string pick = Console.In.ReadLine();
                if (pick != "1" && pick != "2")
                {
                    Console.WriteLine("Wrong value inserted. Going back to main menu.");
                    Console.In.ReadLine(); //No guarda el input, es una pausa.
                    return;
                }
                else if (pick == "1")
                {
                    WriteVehicleData();
                }
                else WriteVehicleRandom();
            }

            long CheckAvailability()
            {
                for (int i = 0; i < NormalParking.Length; i++)
                {
                    if (NormalParking[i].model == "Deleted")
                    {
                        return i;
                    }
                }
                return -1;
            }

            void WriteVehicleData()
            {
                Car newCar = new Car();
                if (newCar.load() == true)
                {
                    if (CheckAvailability() == -1)
                    {
                        QuantumParking.Add(newCar);
                    }
                    else
                    {
                        NormalParking[CheckAvailability()] = newCar;
                    }
                }
            }

            void WriteVehicleRandom()
            {
                Car newCar = new Car();
                if (CheckAvailability() == -1)
                {
                    QuantumParking.Add(newCar);
                }
                else
                {
                    NormalParking[CheckAvailability()] = newCar;
                }
            }

            void RemoveVehicleByLP()
            {
                Console.Out.WriteLine("Enter a plate number");
                long plateValue = Convert.ToInt64(Console.In.ReadLine());
                string plate = "apert" + plateValue;

                foreach (Car Vehicle in NormalParking)
                {
                    if (Vehicle.licensePlate == plate)
                    {
                        Vehicle.model = "Deleted"; //sets model as deleted so its ignored & able to be overwritten
                    }
                }
                foreach (Car Vehicle in QuantumParking)
                {
                    if (Vehicle.licensePlate == plate)
                    {
                        QuantumParking.Remove(Vehicle);
                    }
                }
                Console.Out.WriteLine("(Press any key) ");
                Console.In.ReadLine(); //No guarda el input, es una pausa.
            }

            void RemoveVehicleByDNI(){
                Console.WriteLine("Enter DNI");
                long dniValue = Convert.ToInt64(Console.In.ReadLine());
                foreach (Car Vehicle in NormalParking)
                {
                    if (Vehicle.carOwner.dni == dniValue)
                    {
                        Vehicle.model = "Deleted"; //sets model as deleted so its ignored & able to be overwritten
                    }
                }
                foreach (Car Vehicle in QuantumParking)
                {
                    if (Vehicle.carOwner.dni == dniValue)
                    {
                        QuantumParking.Remove(Vehicle);
                    }
                }
                Console.Out.WriteLine("(Press any key) ");
                Console.In.ReadLine(); //No guarda el input, es una pausa.
            }
        }
    }
}


/*
 * Un programa para administrar estacionamiento. 
 * 2 Estacionamientos, un estatico de 12 (array prob), y ahi las posiciones 3 , 7 , 12 estan reservadas
 * para VIPs, solo ocupables por vehiculos con dueños vip (clase con isVip?)
 * El otro es infinito (dinamico) pero las posiciones pueden tener difs tamaños.
 * Estos tamaños son mini, standard, o max. un vehiculo mas chico puede entrar en un coso mas grande pero no es ideal.
 * (probablemente hay que chequear que agujeros hay)
 * 
 * Se nos pide intentar llenar el estacionamiento regular antes del cuantico. no hace falta mover los vehic
 * una vez estacionados. 
 * 
 * Cada vehiculo tiene modelo, dueño, matricula, y dimensiones guardadas por ancho y largo. (y el isVip)
 * si tiene menos de 4 metros de largo y 1.5 de ancho se considera mini. si tien(1.5 a 2 de ancho
 * es std, y si excede cualquiera de las dimensiones se considera max (conviene chequear de max a min)
 * 
 * Se pide que el sistema pueda generar automaticamente muchos vehiculos y posiciones del estacionamiento  con atrib
 * aleatorios. (probablemente en el cuantico)
 * 
 */

//la cosa de los espacios y sus tamaños. 
//los load tampoco respetan los vip spaces