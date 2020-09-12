using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroBase.MACRO
{
    public class TMacroBase
    {
        public TMacroList parentlist;
        public int index;
        public bool CheckedFlg = false;
        public string MacroParamStr = "";
        public int CommandNo = -1;
        public string[] Param = null;

        public T GetDat<T>(int index)
        {
            object obj = null;

            if (typeof(T) == typeof(int))
            {
                obj = -1;
                if (Param.Length <= index) { return (T)obj; }
                if (Microsoft.VisualBasic.Information.IsNumeric(Param[index]) == false) { return (T)obj; }
                obj = Convert.ToInt32(Param[index]);
            }
            else if (typeof(T) == typeof(double))
            {
                obj = -1.0;
                if (Param.Length <= index) { return (T)obj; }
                if (Microsoft.VisualBasic.Information.IsNumeric(Param[index]) == false) { return (T)obj; }
                obj = Convert.ToDouble(Param[index]);
            }
            else if (typeof(T) == typeof(string))
            {
                obj = "";
                if (Param.Length <= index) { return (T)obj; }
                obj = Param[index].ToString();
            }

            return (T)obj;
        }

        public TMacroBase(TMacroList parent, string _MacroParamStr, int _CommandNo, string[] _Param)
        {
            parentlist = parent;
            index = parentlist.Count;
            CheckedFlg = false;
            MacroParamStr = _MacroParamStr;
            CommandNo = _CommandNo;
            Param = _Param;
        }
    }

    public class TMacroList : List<TMacroBase>
    {

    }
}
