/**
* @author Lee Grey / Jonathan Hopcroft
* 
* seeded random number generator
* based on Rndm by Grant Skinner
* https://github.com/gskinner/AS3Libs/blob/master/Rndm/com/gskinner/utils/Rndm.as
*
* Incorporates implementation of the Park Miller ( 1988 ) "minimal standard" linear 
* congruential pseudo-random number generator by Michael Baczynski, www.polygonal.de.
* ( seed * 16807 ) % 2147483647 
*
* Converted to C# and expanded by By Lee Grey and Jonathan Hopcroft
*
* Original copyright notice:

* Copyright (c) 2008 Grant Skinner
* 
* Permission is hereby granted, free of charge, to any person
* obtaining a copy of this software and associated documentation
* files (the "Software"), to deal in the Software without
* restriction, including without limitation the rights to use,
* copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the
* Software is furnished to do so, subject to the following
* conditions:
* 
* The above copyright notice and this permission notice shall be
* included in all copies or substantial portions of the Software.
* 
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
* EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
* OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
* NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
* HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
* WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
* FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
* OTHER DEALINGS IN THE SOFTWARE.
*
*/

using System;
using UnityEngine;

public class SeededRandom : MonoBehaviour {
    
    private const int Multiplier = 16807; // this is a float so it can contain the massive value
    private const int Modulus = 2147483647;

    protected long initialSeed;
    protected long currentSeed;
    
    protected int useCount = 0;

    public static bool reportUsecountFlag = false;
    
    /**
		* 
		* @param seed cannot be zero
		*/        
    public SeededRandom(uint seed = 1)
    {
        currentSeed = initialSeed = seed;
    }

    // accept int seeds for convenience
    public SeededRandom(int seed = 1)
    {
        currentSeed = initialSeed = seed;
    }

    /**
        * 
        * @param seed cannot be zero
        */
    public void setSeed(uint seed)
    {
        if (reportUsecountFlag)
        {
            ReportUseCount();
        }
        currentSeed = initialSeed = seed;
        useCount = 0;
    }

    public void ReportUseCount()
    {
        Console.WriteLine("Random has been used " + useCount + " times since last set seed.");
    }
    
    /**
		* returns an integer 0 <= n < int.MAX_VALUE, 
		* or 0 <= n < limit if specified
		* @param	limit the exclusive upper limit to the result ( must be positive )
		* @return
		*/
    public int Next()
    {
        useCount++;
        currentSeed = (currentSeed * Multiplier) % Modulus;
        return (int)currentSeed;
    }

    /**
		* returns a number 0 <= n < 1, 
		* or 0 <= n < limit if specified
		* @param	limit the exclusive upper limit to the result ( must be positive )
		* @return
		*/
    public float NextFloat()
    {
        return (float)Next() / (int.MaxValue - 1);
    }

    // duplicate UnityEngine.Random's value getter:
    public float value { get { return NextFloat();  } private set { } }
    
    public int Range(int min, int max)
    {
        // make sure min is less than max
        if (min > max)
        {
            int tmp = max;
            max = min;
            min = tmp;
        }

        if (min == max)
        {
            return min;
        }
        
        return (Next() % (max - min)) + min;
    }

    public float Range(float min, float max)
    {
        // make sure min is less than max
        if (min > max)
        {
            float tmp = max;
            max = min;
            min = tmp;
        }
        return (NextFloat() * (max - min)) + min;
    }

    /**
		* returns either -1 or 1
		* Use for assigning a random sign to a value.
		* @return
		*/
    public int sign()
    {
        if (NextFloat() < 0.5)
        {
            return 1;
        }
        else {
            return -1;
        }
    }

}