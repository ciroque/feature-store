/*****************************************************************
 * Copyright 2008 Microsoft Corporation
 * File    : NumberRange.cs
 * Created : 2008-06-18
 * Author  : danieln
 * Purpose : Range definition for numbers
 * 
 * Change History :
 *  Date        Author      Change
 *  -------------------------------
 *  2008-06-19  danieln     Created
 *  2008-06-20  danieln     Function complete
 * 
 * Notes :
 *****************************************************************/
using System;

namespace ChartUtility
{
    class NumberRange : Range
    {
        private float[] gScale = new float[] { 0.0010f, 0.0020f, 0.0025f, 0.0050f };
        protected float _Step;

        protected float _Min;
        protected float _Max;

        public override object Min
        { get { return _Min; } set { _Min = float.Parse(value.ToString()); AdjustStep(); } }
        public override object Max
        { get { return _Max; } set { _Max = float.Parse(value.ToString()); AdjustStep(); } }
        public override object Diff
        { get { return _Max - _Min; } }
        public override float FloatDiff
        { get { return (float)Diff; } }

        public NumberRange(float min, float max)
        {
            _Min = min; _Max = max;
            AdjustStep();
        }
        public NumberRange()
        {
            _Min = 0; _Max = 0;
        }
        public override float Normalize(object val)
        {
            return (float.Parse(val.ToString())-_Min)/FloatDiff;
        }

        public override void AdjustStep()
        {
            _Step = BestFitScale();
            _StepCount = (int)(FloatDiff / _Step) + 1;
        }
        public bool SmartExpand(NumberRange range)
        {
            return SmartExpand((Range)range);
        }
        public override bool SmartExpand(Range range)
        {
            float[] ranges = new float[] { (_Max - _Min),
                    ((float)range.Max-(float)range.Min) };
            if (Math.Min(ranges[0], ranges[1]) / Math.Max(ranges[0], ranges[1]) > 0.15f
                && Math.Abs((ranges[0] / 2 + _Min) - (ranges[1] / 2 + (float)range.Min)) < Math.Min(ranges[0], ranges[1]) / 1)
            {
                Min = Math.Min((float)range.Min, _Min);
                Max = Math.Max((float)range.Max, _Max);
                range.Min = Min;
                range.Max = Max;
                return true;
            }
            else
                return false;
        }
        public override float GetStepFloat(int step)
        {
            return (float)GetStep(step)-_Min;
        }
        public override object GetStep(int step)
        {
            return _Min + _Step * step;
        }
        public float BestFitScale()
        {
            int p = 1;
            int i = 0;
            for (i = 0; (_Max - _Min) / (gScale[i % 4] * p) > 11; i++)
                if (i % 4 == 0)
                    p *= 10;
                else
                    continue;

            return gScale[i % 4] * p;
        }
        public float SnapToScale(float var)
        {
            return SnapToScale(var, false);
        }
        public float SnapToScale(float var, bool floor)
        {
            float step = BestFitScale();
            if (var < 0)
                step *= -1;
            float iter = -step;
            if(floor)
                while (Math.Abs(iter + step) < Math.Abs(var))
                    iter += step;
            else
                while (Math.Abs(iter) < Math.Abs(var))
                    iter += step;
            return iter;
        }
        public override Range SnapToScale()
        {
            return new NumberRange(SnapToScale(_Min), SnapToScale(_Max));
        }
        public override Range ExpandTo(object min, object max)
        {
            return ExpandTo((float)min, (float)max);
        }
        public Range ExpandTo(float min, float max)
        {
            return new NumberRange(Math.Min(min, _Min), Math.Max(max, _Max));
        }
        //public override Range ExpandTo(Range range)
        //{
        //    return new NumberRange(Math.Min((float)range.Min, _Min), Math.Max((float)range.Max, _Max));
        //}
    }
}
