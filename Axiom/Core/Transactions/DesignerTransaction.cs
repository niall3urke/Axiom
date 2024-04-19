using System.ComponentModel.Design;

namespace Axiom.Core.Transactions
{

    public delegate object TransactionMethod(IDesignerHost host, object sender);

    public abstract class DesignerTransactionUtility
    {
        public static object DoTransaction(IDesignerHost host, string transactionName, TransactionMethod method, object sender)
        {
            DesignerTransaction transaction = null;

            object result = null;

            try
            {
                transaction = host.CreateTransaction(transactionName);
                result = method(host, sender);
            }
            catch (CheckoutException e)
            {
                if (e != CheckoutException.Canceled)
                {
                    throw e;
                }
            }
            catch
            {
                if (transaction != null)
                {
                    transaction.Cancel();
                    transaction = null; // Prevents commit in finally block
                }
            }
            finally
            {
                if (transaction != null)
                {
                    transaction.Commit();
                }
            }

            return result;
        }


    }
}
