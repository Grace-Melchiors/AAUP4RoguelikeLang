using System;


namespace Antlr_language.ast
{
    public static class Enums
    {
        public enum Types {
            BOOL,
            INTEGER,
            MAP
        }
        public enum Operators {
            add,
            sub,
            mult,
            div,
            eq,
            neq,
            geq,
            leq,
            less,
            greater,
            and,
            or,
            not,
            none
        }
        public static string OperatorToString (Operators val) {
            string result = "";
            switch (val) {
                case Operators.add:
                    result = "+";
                    break;
                case Operators.sub:
                    result = "-";
                    break;
                case Operators.mult:
                    result = "*";
                    break;
                case Operators.div:
                    result = "/";
                    break;
                case Operators.eq:
                    result = "==";
                    break;
                case Operators.neq:
                    result = "!=";
                    break;
                case Operators.geq:
                    result = ">=";
                    break;
                case Operators.leq:
                    result = "<=";
                    break;
                case Operators.less:
                    result = "<";
                    break;
                case Operators.greater:
                    result = ">";
                    break;
                case Operators.and:
                    result = "&&";
                    break;
                case Operators.or:
                    result = "||";
                    break;
                case Operators.not:
                    result = "!";
                    break;
                case Operators.none:
                    result = "";
                    break;
            }
            return result;
        }
        public static Operators StringToOperator (string val) {
            Operators result = Operators.none;
            switch (val) {
                case "+":
                    result = Operators.add;
                    break;
                case "-":
                    result = Operators.sub;
                    break;
                case "*":
                    result = Operators.mult;
                    break;
                case "/":
                    result = Operators.div;
                    break;
                case "==":
                    result = Operators.eq;
                    break;
                case "!=":
                    result = Operators.neq;
                    break;
                case ">=":
                    result = Operators.geq;
                    break;
                case "<=":
                    result = Operators.leq;
                    break;
                case "<":
                    result = Operators.less;
                    break;
                case ">":
                    result = Operators.greater;
                    break;
                case "&&":
                    result = Operators.and;
                    break;
                case "||":
                    result = Operators.or;
                    break;
                case "!":
                    result = Operators.not;
                    break;
                case "":
                    result = Operators.none;
                    break;
            }
            return result;
        }
    }
}


