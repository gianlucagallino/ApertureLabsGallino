using System.ComponentModel.Design;
using System.Linq;

namespace ApertureLabsGallino
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Autor: Gallino Gianluca.
            //Pido perdon de antemano por el desastre que va a pasar por sus ojos en instantes. Se me complico el tema del espacio infinito + tamaños.

            bool isRunning = true;
            string menuPick;
            Car[] NormalParking = new Car[12];
            List<Car> QuantumParking = new List<Car>();
            List<Car> QuantumTemporary = new List<Car>();  
            List<long> QuantumSpaces = new List<long>();


            LoadRandomData();

            while (isRunning)
            {

                Console.Clear();
                Console.Out.WriteLine("\n             Welcome to the Aperture Science            ");
                Console.Out.WriteLine(" Parking Entry Non-symmetric Infinite System! (P.E.N.I.S).\n");
                Console.Out.WriteLine("                     Management Menu                      ");
                Console.Out.WriteLine(" ---------------------------------------------------------");
                Console.Out.WriteLine("                  1. List all vehicles                    ");
                Console.Out.WriteLine("                   2. Add new vehicle                     ");
                Console.Out.WriteLine("            3. Remove vehicle by License plate            ");
                Console.Out.WriteLine("              4. Remove vehicle by owner DNI              ");
                Console.Out.WriteLine("            5. Remove random amount of vehicles           ");
                Console.Out.WriteLine("                6. Optimise parking spaces                ");
                Console.Out.WriteLine(" ---------------------------------------------------------");
                Console.Out.WriteLine("                  Please enter your Pick:                 ");

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
                        RemoveRandomAmount();
                        break;
                    case "6":
                        OptimiseParkingSpaces();
                        break;
                    default:
                        Console.Clear();
                        Console.Out.WriteLine(" Pay attention to your inputs, please. (Press any key) ");
                        Console.In.ReadLine(); //No guarda el input, es una pausa.
                        break;
                }
            }


            void LoadRandomData()
            {
                for (int i = 0; i < NormalParking.Length; i++) //Esto llena el estacionamiento normal, para poder llegar a usar el quantum 
                {
                    NormalParking[i] = new Car();
                    if ((i == 3 || i == 7 || i == 12) && (NormalParking[i].CheckVip() == false))
                    {
                        while (NormalParking[i].CheckVip() == false)
                        {
                            QuantumTemporary.Add(NormalParking[i]); //si no es vip, lo patea a la n dimension.
                            NormalParking[i] = new Car();
                        }
                    }
                }

                Random RNG= new Random();
                long QuantumToOccupy = RNG.Next(1, 100);
                
                //Este loop arma espacios de 1 de las 3 categorias. 1 = min 2=prom 3=max
                for (int i = 0; i < QuantumToOccupy; i++)
                {
                    QuantumSpaces.Add(RNG.Next(1, 4));
                }

                //y este arma los autos, mandandolos al temp
                for (int i = 0; i < QuantumToOccupy; i++)
                {
                    QuantumTemporary.Add(new Car());
                }

                //este otro intenta acomodar los autos en sus respectivos lugares. 
                //si, es un spaghetti. no se me ocurrio otra forma :(
                foreach (Car Vehicle in QuantumTemporary)
                {
                    long count = 0;
                    foreach(long Space in QuantumSpaces)
                    {
                        count++;
                        if (Vehicle.sizeType == Space && QuantumSpaces.ElementAt((Index)(count-1))==0) //Esto revisa que ese espacio no este ocupado. 
                        {
                            QuantumSpaces[QuantumSpaces.Count - 1] = 1;
                            QuantumParking.Add(Vehicle);
                            QuantumTemporary.Remove(Vehicle);
                        }
                    }
                }

                //Aca genera nuevos espacios para los autos restantes, hasta que todos tengan un lugar que les corresponde. 

                if (QuantumTemporary.Count > 0)
                {
                    while (QuantumTemporary.Count > 0)
                    {
                        QuantumSpaces.Add(RNG.Next(1, 4)); //Genera una nueva plaza con valor 1-3
                        if (QuantumSpaces[QuantumSpaces.Count - 1] == QuantumTemporary[0].sizeType) //esto se fija si el nuevo espacio matchea con el primer elemento de el temporary
                        {
                            QuantumParking.Add(QuantumTemporary[0]);
                            QuantumTemporary.RemoveAt(0);
                        }
                    }
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
                        if (CheckAvailability() == 3 || CheckAvailability() == 7 || CheckAvailability() == 12)
                        {
                            if (newCar.carOwner.isVip== true) NormalParking[CheckAvailability()] = newCar;
                           else QuantumTemporary.Add(newCar);
                            newCar.model = "Deleted";
                        }
                        else
                        {
                            NormalParking[CheckAvailability()] = newCar;
                        }
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
                    if (CheckAvailability() == 3 || CheckAvailability() == 7 || CheckAvailability() == 12)
                    {
                        if (newCar.carOwner.isVip == true) NormalParking[CheckAvailability()] = newCar;
                        else QuantumTemporary.Add(newCar);
                        newCar.model = "Deleted";
                    }
                    else
                    {
                        NormalParking[CheckAvailability()] = newCar;
                    }
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
                for (int i = 0; i<QuantumParking.Count;i++)
                {
                    if (QuantumParking[i].licensePlate == plate)
                    {
                        QuantumParking.Remove(QuantumParking[i]);
                        break;
                    }
                }
                Console.Out.WriteLine("(Press any key) ");
                Console.In.ReadLine(); //No guarda el input, es una pausa.
            }

            void RemoveVehicleByDNI()
            {
                Console.WriteLine("Enter DNI");
                long dniValue = Convert.ToInt64(Console.In.ReadLine());
                foreach (Car Vehicle in NormalParking)
                {
                    if (Vehicle.carOwner.dni == dniValue)
                    {
                        Vehicle.model = "Deleted"; //sets model as deleted so its ignored & able to be overwritten
                    }
                }
                for (int i = 0; i < QuantumParking.Count; i++)
                {
                    if (QuantumParking[i].carOwner.dni == dniValue)
                    {
                        QuantumParking.Remove(QuantumParking[i]);
                    }
                }
                Console.Out.WriteLine("(Press any key) ");
                Console.In.ReadLine(); //No guarda el input, es una pausa.
            }

            void RemoveRandomAmount()
            {
                Random RNG = new Random();
                long NormalDeleteAmount = RNG.Next(0, NormalParking.Length - 1);
                long QuantumDeleteAmount = RNG.Next(0, QuantumParking.Count - 1);

                for (int i = 0; i < NormalDeleteAmount; i++)
                {
                    NormalParking[RNG.Next(0, NormalParking.Length - 1)].model = "Deleted";
                }
                for (int i = 0; i < QuantumDeleteAmount; i++)
                {
                    QuantumParking.RemoveAt(RNG.Next(0, QuantumParking.Count - 1));
                }
            }

            void OptimiseParkingSpaces()
            {
                for (int i = 0; i<QuantumParking.Count;i++)
                {
                    if (QuantumParking[i].sizeType != QuantumSpaces[i])
                    {
                        QuantumSpaces[i] = QuantumParking[i].sizeType; //esto convierte el espacio en el que esta, en uno de su tipo, ya que en un espacio infinito, todo existe al mismo tiempo. 
                                                                       //tambien conocido como "llevale la montaña a mahoma"
                    }
                }
                Console.Out.WriteLine("(Press any key) ");
                Console.In.ReadLine(); //No guarda el input, es una pausa.
            }
        }
    }
}
