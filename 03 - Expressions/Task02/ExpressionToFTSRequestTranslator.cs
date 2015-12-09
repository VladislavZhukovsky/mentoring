using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Sample03
{
    public class ExpressionToFTSRequestTranslator : ExpressionVisitor
    {
        StringBuilder resultString;

        public string Translate(Expression exp)
        {
            resultString = new StringBuilder();
            Visit(exp);

            return resultString.ToString();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.DeclaringType == typeof(Queryable))
            {
                switch(node.Method.Name)
                {
                    case "Where":
                        var predicate = node.Arguments[1];
                        Visit(predicate);
                        return node;
                }
            }
            if (node.Method.DeclaringType == typeof(string))
            {
                Visit(node.Object);
                resultString.Append("(");
                switch (node.Method.Name)
                {
                    case "StartsWith":
                        var constant = node.Arguments[0];
                        Visit(constant);
                        resultString.Append("*");
                        break;
                    case "EndsWith":
                        resultString.Append("*");
                        constant = node.Arguments[0];
                        Visit(constant);
                        break;
                    case "Contains":
                        resultString.Append("*");
                        constant = node.Arguments[0];
                        Visit(constant);
                        resultString.Append("*");
                        break;
                    default:
                        throw new NotSupportedException("Not supported operation");
                }
                resultString.Append(")");
                return node;
            }
            return base.VisitMethodCall(node);
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            switch (node.NodeType)
            {
                case ExpressionType.Equal:
                    if (node.Left.NodeType == ExpressionType.Constant)
                    {
                        Visit(node.Right);
                        resultString.Append("(");
                        Visit(node.Left);
                    }
                    else
                    {
                        //if (!(node.Left.NodeType == ExpressionType.MemberAccess))
                        //    throw new NotSupportedException(string.Format("Left operand should be property or field", node.NodeType));

                        //if (!(node.Right.NodeType == ExpressionType.Constant))
                        //    throw new NotSupportedException(string.Format("Right operand should be constant", node.NodeType));

                        Visit(node.Left);
                        resultString.Append("(");
                        Visit(node.Right);
                    }
                    resultString.Append(")");
                    break;
                default:
                    throw new NotSupportedException(string.Format("Operation {0} is not supported", node.NodeType));
            };

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            resultString.Append(node.Member.Name).Append(":");

            return base.VisitMember(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            resultString.Append(node.Value);

            return node;
        }
    }
}
