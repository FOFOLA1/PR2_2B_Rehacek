namespace Test
{
    using System;

    class Program
    {
        public static void Main(string[] args)
        {
            Test();
        }
        public static void Test()
        {
            VacuumCleaner lux = new VacuumCleaner();
            Console.WriteLine(lux.TurnOn()); //vypíše Connected to 230 V and cleaning
            Console.WriteLine(lux.TurnOff()); //vypíše Cleaning finished

            CircularSaw sawPowerful = new CircularSaw(400);
            CircularSaw sawWeak = new CircularSaw(230);

            Lamp lamp = new Lamp(230, 7); //lampa na 230 V, dosvítí 7 m
            Console.WriteLine(lamp.TurnOn()); //vypíše "Light is on"
            Console.WriteLine(lamp.TurnOff()); //vypíše "Light is off"

            ElectricDevice[] tools = { lux, sawPowerful, sawWeak, lamp };
            Gadgets gadgetSet = new Gadgets(tools);
            Console.WriteLine(gadgetSet.MaxVoltage); //vypíše 400 - nejsilnější je pila
            gadgetSet.TurnAllOn(); //vypíše postupně zapínací zvuk všech čtyř spotřebičů
            gadgetSet.TurnAllOff(); //zase všechny povypíná


            Torch torch = new Torch(3);
            WillOWisp fairy = new WillOWisp();

            ILightSource[] lights = { torch, fairy, lamp };
            lamp.TurnOn();
            Console.WriteLine(EnlightedPath(lights)); //vypíše 21
            lamp.TurnOff();
            Console.WriteLine(EnlightedPath(lights)); //vypíše 7

        }

        public static double EnlightedPath(ILightSource[] poleZdroju)
        {
            //Vrátí, jak dlouhou cestu by polem zdrojů šlo osvítit
            //takže pokud mám dva zdroje - 7m a 1 m, pak osvítím až 16 m cesty
            //(zdroj svítí na obě strany)
            double count = 0;
            foreach (ILightSource zdroj in poleZdroju)
            {
                count += zdroj.EnlightedDistance * 2;
            }
            return count;
        }
    }

    //zde vložte vaše třídy a interface
    //nevkládejte kód s namespace

    interface ILightSource
    {
        double EnlightedDistance { get; }
    }

    class Torch : ILightSource
    {
        public double EnlightedDistance { get; private set; }

        public Torch(double enlightedDistance)
        {
            EnlightedDistance = enlightedDistance;
        }
    }

    class WillOWisp : ILightSource
    {
        public double EnlightedDistance { get; private set; }
        public WillOWisp()
        {
            EnlightedDistance = 0.5;
        }
    }

    abstract class ElectricDevice
    {
        public int Voltage { get; private set; }
        public string Name { get; private set; }

        protected ElectricDevice(int voltage, string name)
        {
            Voltage = voltage;
            Name = name;
        }

        public abstract string TurnOn();
        public abstract string TurnOff();
    }

    sealed class VacuumCleaner : ElectricDevice
    {
        public VacuumCleaner() : base(230, "Vacuum cleaner")
        {
        }

        public override string TurnOn()
        {
            return $"Connected to {this.Voltage} V and cleaning";
        }
        public override string TurnOff()
        {
            return "Cleaning finished";
        }
    }

    sealed class CircularSaw : ElectricDevice
    {
        public CircularSaw(int voltage) : base(voltage, "Circular saw")
        {
        }

        public override string TurnOn()
        {
            return $"Connected to {this.Voltage} V and cutting";
        }
        public override string TurnOff()
        {
            return "Ohhh, that silence";
        }
    }

    sealed class Lamp : ElectricDevice, ILightSource
    {
        private double distance;
        public double EnlightedDistance
        {
            get
            {
                if (isTurnedOn) return distance;
                return 0;
            }
            private set { distance = value; }
        }
        public bool isTurnedOn = false;

        public Lamp(int voltage, double enlightedDistance) : base(voltage, "Lamp")
        {
            EnlightedDistance = enlightedDistance;
            Console.WriteLine($"lampa na {voltage} V, dosvítí {enlightedDistance} m");
        }

        public override string TurnOn()
        {
            isTurnedOn = true;
            return "Light is on";
        }
        public override string TurnOff()
        {
            isTurnedOn = false;
            return "Light is off";
        }
    }

    class Gadgets
    {
        private ElectricDevice[] _devices;

        public Gadgets(ElectricDevice[] devices)
        {
            _devices = devices;
        }

        public int MaxVoltage
        {
            get
            {
                int max = 0;
                foreach (ElectricDevice device in _devices)
                {
                    if (max < device.Voltage) max = device.Voltage;
                }
                return max;
            }
        }

        public void TurnAllOn()
        {
            foreach (ElectricDevice device in _devices)
            {
                Console.WriteLine(device.TurnOn());
            }
        }
        public void TurnAllOff()
        {
            foreach (ElectricDevice device in _devices)
            {
                Console.WriteLine(device.TurnOff());
            }
        }
    }
}
