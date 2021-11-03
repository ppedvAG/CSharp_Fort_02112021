using System;
using System.Collections.Generic;

namespace HalloGenerics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            object[] fünfDts = GibMir5Davon(typeof(DateTime));
            object[] fünfInt = GibMir5Davon(typeof(int));

            int[] fünfInt2 = GibMir5Davon<int>();
            DateTime[] fünfIntDTs = GibMir5Davon<DateTime>();

            //MethodeMitGenerics(12m, "Hallo");
            MethodeMitGenerics(new MyClass2(), "Hallo");
        }


        static void MethodeMitGenerics<EineZahl, EinText>(EineZahl zahl, EinText einText) where EineZahl : MyClass
        {

        }

        static object[] GibMir5Davon(Type vorlage)
        {
            object t1 = Activator.CreateInstance(vorlage);
            object t2 = Activator.CreateInstance(vorlage);
            object t3 = Activator.CreateInstance(vorlage);
            object t4 = Activator.CreateInstance(vorlage);
            object t5 = Activator.CreateInstance(vorlage);

            return new[] { t1, t2, t3, t4, t5 };
        }

        static EinType[] GibMir5Davon<EinType>()
        {
            //EinType t1 = (EinType)Activator.CreateInstance(typeof(EinType));
            //EinType t2 = (EinType)Activator.CreateInstance(typeof(EinType));
            //EinType t3 = (EinType)Activator.CreateInstance(typeof(EinType));
            //EinType t4 = (EinType)Activator.CreateInstance(typeof(EinType));
            //EinType t5 = (EinType)Activator.CreateInstance(typeof(EinType));

            EinType t1 = Activator.CreateInstance<EinType>();
            EinType t2 = Activator.CreateInstance<EinType>();
            EinType t3 = Activator.CreateInstance<EinType>();
            EinType t4 = Activator.CreateInstance<EinType>();
            EinType t5 = Activator.CreateInstance<EinType>();

            return new EinType[] { t1, t2, t3, t4, t5 };
        }

    }

    class MyClass
    {

    }

    class MyClass2 : MyClass
    {

    }
}
