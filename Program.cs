namespace ApertureLabsGallino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Gallino Gianluca

            bool isRunning = true;
            string menuPick;

            while (isRunning)
            {

                Console.Clear();  //Limpia la consola, para mas prolijidad
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

                switch (menuPick)                                                               //Se que el Switch causa alergia, pero pienso que en un menu encuentra uso apropiado. 
                {
                    case "1":
                        //ListAllVehicles()
                        break;
                    case "2":
                        //AddNewVehicle();
                        break;
                    case "3":
                        //RemoveVehicleByLP();
                        break;
                    case "4":
                        //RemoveVehicleByDNI();
                        break;
                    case "5":
                        //RemoveRandomAmount();
                        break;
                    case "6":
                        //OptimiseParkingSpaces();
                        break;
                    default:
                        Console.Out.WriteLine(" Pay attention to your inputs, please. (Press any key) ");
                        Console.In.ReadLine(); //No guarda el input, es una pausa.
                        break;
                }
            }
        }
    }
}

/*
 * Un programa para administrar estacionamiento. 
 * 2 Estacionamientos, un estatico de 12 (array prob), y ahi las posiciones 3 , 7 , 12 estan reservadas
 * para VIPs, solo ocupables por vehiculos con dueños vip (clase con isVip?)
 * El otro es infinito (dinamico) pero las posiciones no son simetricas (????) y pueden tener difs tamaños.
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
 * Funcionalidades:
 * -Listar todos los vehiculos
 * -Agregar un vehiculo (con atributos aleatorios o no)
 * -Remocer un vehiculo en base a su numero de matricula
 * -Remover un vehiculo en base al dni del dueño
 * -Remover una cant aleatoria de vehiculos. 
 * -Optimizar el espapcio, moviendo todos los vehiculos que no esten ocupando una casilla corespondiente
 * de su espacio actual a uno nuevo (se puede poner en un temp)
 * 
 * 
 * So...
 * -Generar clase Car
 * Armar el estacionamiento 1 
 * Armar el estacionamiento 2 con rng
 */