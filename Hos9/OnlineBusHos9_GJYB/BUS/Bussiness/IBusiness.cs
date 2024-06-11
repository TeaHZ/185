using CommonModel;

namespace OnlineBusHos9_GJYB.BUS
{
    public interface IBusiness
    {
        DataReturn PSNQUERY(string injson);
        DataReturn REGTRY(string injson);
        DataReturn OUTPTRY(string injson);
        DataReturn SETTLE(string injson);
        DataReturn REFUND(string injson);
        DataReturn COMMON(string injson);

    }
}
