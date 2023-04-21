

namespace BussinesLogic
{
    public class FeedValue
    {       

        private static FeedValue _instance = null;

        private decimal _randomDecimal;
        private DateTime _startDate = DateTime.Now;
        private bool _flag;
        private int _resetFeed = 59 ;
       
        private static readonly object lockObj = new object();
        private FeedValue() 
        {
            var random = new Random();
            byte nbyte = (byte)random.Next(0, 2);

            _randomDecimal = new decimal(random.NextDouble());
            _randomDecimal = Math.Round((decimal)_randomDecimal, 2);
            _startDate = DateTime.Now;
            _flag = true;
        }

        public static FeedValue Instance 
        {
            get 
            {
                lock (lockObj)
                {
                    var now = DateTime.Now;
                    TimeSpan diffdates = default;                    

                    if (_instance == null )
                    {
                        _instance = new FeedValue();                         
                      
                    }
                    if (_instance.Flag)
                    {
                        diffdates = now - _instance.StartDate;
                        var minutes = diffdates.TotalMinutes;
                        if (minutes > _instance.ResetFeed) 
                        {
                            _instance = new FeedValue();
                        }
                    }
                   
                    
                }
                return _instance;
            }
            
        }
        

        public decimal RandomDecimal { get { return _randomDecimal; } }
        private DateTime StartDate { get { return _startDate; } }
        private bool Flag { get { return _flag; } }
        private int ResetFeed { get { return _resetFeed; } }
    }
}
