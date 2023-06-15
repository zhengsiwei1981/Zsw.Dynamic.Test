using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Zsw.Dynamic.Interface;

namespace Zsw.Dynamic
{
    public class SetParamExpressionVisitor : ExpressionVisitor
    {
        public ParameterExpression Parameter { get; set; }
        public SetParamExpressionVisitor() { }
        public SetParamExpressionVisitor(ParameterExpression parameter)
        {
            this.Parameter = parameter;
        }
        public Expression Modify(Expression exp)
        {
            return this.Visit(exp);
        }
        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            return this.Parameter;
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Lambad<T> where T : class, new()
    {
        public Expression Expr = null;

        private Common<T> Common = new Common<T>();

        public ParameterExpression Parameter
        {
            get;
            set;
        }
        public Lambad()
        {
            this.Parameter = this.Common.GetParameter();
        }
        public Expression<Func<T, bool>> Create()
        {
            if (Expr != null)
            {
                return Expression.Lambda<Func<T, bool>>(Expr, this.Parameter);
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpr"></param>
        /// <param name="value"></param>
        /// <param name="Method"></param>
        public void WithAnd<TProperty>(Expression<Func<T, TProperty>> propertyExpr, object value, IMethod Method)
        {
            if (value != null && value.ToString().Length >= 1)
            {
                Init(propertyExpr, value);
                if (Expr == null)
                {
                    Expr = Method.Build<T>(Common.GetProperty(), Common.GetConstant());
                }
                else
                {
                    Expr = Expression.And(Expr, Method.Build<T>(Common.GetProperty(), Common.GetConstant()));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryItems"></param>
        public void WithAnd(Func<Lambad<T>, Expression> func)
        {
            var fExpr = func(new Lambad<T>());
            if (fExpr == null)
            {
                return;
            }
            if (this.Expr == null)
            {
                this.Expr = fExpr;
            }
            else
            {
                SetParamExpressionVisitor visitor = new SetParamExpressionVisitor(this.Parameter);

                this.Expr = visitor.Modify(this.Expr);
                fExpr = visitor.Modify(fExpr);
                this.Expr = Expression.And(this.Expr, fExpr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fExpr"></param>
        public void WithAnd(Expression fExpr)
        {
            if (fExpr == null)
            {
                return;
            }
            if (this.Expr == null)
            {
                SetParamExpressionVisitor visitor = new SetParamExpressionVisitor(this.Parameter);
                this.Expr = visitor.Modify(fExpr);
            }
            else
            {
                SetParamExpressionVisitor visitor = new SetParamExpressionVisitor(this.Parameter);

                this.Expr = visitor.Modify(this.Expr);
                fExpr = visitor.Modify(fExpr);
                this.Expr = Expression.And(this.Expr, fExpr);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Item"></param>
        public void WithAnd(QueryItem Item)
        {
            if (Item.CanCreate)
            {
                Common.Set(Item.Name, Item.Value);
                if (Expr == null)
                {
                    Expr = Item.Method.Build<T>(Common.GetProperty(), Common.GetConstant());
                }
                else
                {
                    Expr = Expression.And(Expr, Item.Method.Build<T>(Common.GetProperty(), Common.GetConstant()));
                }
            }
        }
        /// 
        /// </summary>
        /// <param name="Item"></param>
        public void WithOr(QueryItem Item)
        {
            if (Item.CanCreate)
            {
                Common.Set(Item.Name, Item.Value);
                if (Expr == null)
                {
                    Expr = Item.Method.Build<T>(Common.GetProperty(), Common.GetConstant());
                }
                else
                {
                    Expr = Expression.Or(Expr, Item.Method.Build<T>(Common.GetProperty(), Common.GetConstant()));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpr"></param>
        /// <param name="value"></param>
        /// <param name="Method"></param>
        public void WithOr<TProperty>(Expression<Func<T, TProperty>> propertyExpr, object value, IMethod Method)
        {
            if (value != null && value.ToString().Length >= 1)
            {
                Init(propertyExpr, value);
                if (Expr == null)
                {
                    Expr = Method.Build<T>(Common.GetProperty(), Common.GetConstant());
                }
                else
                {
                    Expr = Expression.Or(Expr, Method.Build<T>(Common.GetProperty(), Common.GetConstant()));
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="propertyExpr"></param>
        /// <param name="value"></param>
        private void Init<TProperty>(Expression<Func<T, TProperty>> propertyExpr, object value)
        {
            PropertyInfo propertyInfo = (PropertyInfo)(propertyExpr.Body as MemberExpression).Member;
            Common.Set(propertyInfo.Name, value);
        }
    }
}
