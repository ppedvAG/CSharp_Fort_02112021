using System;
using System.Linq;
using System.Collections.Generic;

namespace HalloLinq
{

    delegate void EinfacherDelegate();
    delegate void DelegateMitParameter(string text);
    delegate long CalcDelegate(int a, int b);

    class HalloDelegates
    {
        public HalloDelegates()
        {
            EinfacherDelegate einfacherDelegate = EinfacheMethode;
            EinfacherDelegate einfacherDelegateAno = delegate () { Console.WriteLine("Hallo"); };
            EinfacherDelegate einfacherDelegateAno2 = () => { Console.WriteLine("Hallo"); };
            EinfacherDelegate einfacherDelegateAno3 = () => Console.WriteLine("Hallo");
            Action action = EinfacheMethode;
            Action action1 = () => Console.WriteLine("Hallo");

            DelegateMitParameter delegateMitParameter = MethodeMitPara;
            Action<string> actionMitString = MethodeMitPara;
            Action<string> actionMitStringAno = (string msg) => Console.WriteLine(msg);
            Action<string> actionMitStringAno2 = (msg) => Console.WriteLine(msg);
            Action<string> actionMitStringAno3 = x => Console.WriteLine(x);

            CalcDelegate calc = Sum;
            Func<int, int, long> calcFunc = Sum;
            Func<int, int, long> calcFuncAno = (int a, int b) => { return a + b; };
            Func<int, int, long> calcFuncAno2 = (a, b) => { return a + b; };
            Func<int, int, long> calcFuncAno3 = (a, b) => a + b;
            long res = calcFuncAno2.Invoke(23, 56);

            List<string> texte = new List<string>();
            texte.Where(x => x.StartsWith("b"));
            texte.Where(Filter);


            calc = Substract;
            long result = calc.Invoke(12, 44);
            long result2 = calc(12, 44);
        }
        
        private bool Filter(string arg)
        {
            if (arg.StartsWith("b"))
                return true;
            else
                return false;
            
        }

        long Substract(int a, int b)
        {
            return a - b;
        }

        long Sum(int a, int b)
        {
            return a + b;
        }

        void EinfacheMethode()
        {
            Console.WriteLine("Hallo");
        }

        void MethodeMitPara(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
