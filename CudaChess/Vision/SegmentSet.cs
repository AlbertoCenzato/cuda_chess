using System;
using System.Collections.Generic;

//using Emgu.CV.Structure;

namespace Chess
{
   /*
    class SegmentSet
    {
        private LineSegment2D[] segSet;
        private AngleDS[] angles;

        public SegmentSet(LineSegment2D[] segSet) {
            this.segSet = segSet;
            angles = new AngleDS[37];
            for (int i = 0; i < angles.Length; i++)
                angles[i] = new AngleDS();

            foreach (LineSegment2D seg in this.segSet) {
                double angle = seg.GetAngleDeg();
                int index = mapDegToArrayIndex(angle);
                angles[index].value++;
                angles[index].lines.Add(seg);
            }
        }

        public List<LineSegment2D> GetMaxParallelSet() {
            int maxIndex = 0;
            int maxValue = 0;
            for (int i = 0; i < angles.Length; i++) {
                var angle = angles[i];
                if (angle.value > maxValue) {
                    maxValue = angle.value;
                    maxIndex = i;
                }
            }

            return angles[maxIndex].lines;
        }

        public List<LineSegment2D> GetOrthogonalSet(LineSegment2D line) {
            var angle = line.GetOrthDeg();
            return angles[mapDegToArrayIndex(angle)].lines;
        }

        private int mapDegToArrayIndex(double angle) {
            return (int)Math.Floor((angle + 90) / 5);
        }

        private double mapArrayIndexToDeg(int index) {
            return index * 5 - 90;
        }
            
        private class AngleDS {
            public int value;
            public List<LineSegment2D> lines;

            public AngleDS()
            {
                value = 0;
                lines = new List<LineSegment2D>();
            }
        }
    }
    */
}
