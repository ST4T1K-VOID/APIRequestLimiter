using APITimeOut;
//^ remember this

//you may have some issues if you don't write it this way
APITimeOut.TimeOut timer = new TimeOut(20, false);
APITimeOut.TimeOut timer2 = new TimeOut(20, true);

int count = 21;

//example with throwError == false
while (count > 0)
{
    if (timer.ValidateWithinLimit())
    {
        //do API stuff
        Console.WriteLine("1: Reqeust successful");
        count --;
    }
    else if (timer.ValidateWithinLimit() == false)
    {
        // display error message to user / etc. etc.
        Console.WriteLine("1: Reqeust unsuccessful");
        count--;
    }
}

count = 21;
//example with throwError == true
while (count > 0)
{
    if (timer2.ValidateWithinLimit())
    {
        Console.WriteLine("2: Reqeust successful");
        //do API stuff
    }
    //otherwise; program has stopped/thrown exception
    //watch for the message "Exceeded maximum API requests"	
}
