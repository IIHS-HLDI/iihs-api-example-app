using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;
using IIHSApiApp.Activities;
using IIHSApiApp.Controls;
using IIHSApiApp.Models;

namespace IIHSApiApp.Framework
{
    public class ExpandableListViewAdapter : BaseExpandableListAdapter
    {
        private Context context;
        private List<RatingGroupHeader> listGroup;
        private Dictionary<RatingGroupHeader, List<Java.Lang.Object>> lstChild;

        public ExpandableListViewAdapter(Context context, List<RatingGroupHeader> listGroup, Dictionary<RatingGroupHeader, List<Java.Lang.Object>> lstChild)
        {
            this.context = context;
            this.listGroup = listGroup;
            this.lstChild = lstChild;
        }

        public override int GroupCount
        {
            get
            {
                return listGroup.Count;
            }
        }

        public override bool HasStableIds
        {
            get
            {
                return false;
            }
        }

        public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
        {
            var result = new List<Java.Lang.Object>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            var result = new List<Java.Lang.Object>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);

            if (result == null)
                return 0;
            else
                return result.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            Java.Lang.Object content = GetChild(groupPosition, childPosition);
            var rowItem = RatingChildRowFactory.Create(content);            
            LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            convertView = inflater.Inflate(rowItem.LayoutId, null);
            rowItem.Render(convertView, content);            
            return convertView;
        }

        public override Java.Lang.Object GetGroup(int groupPosition)
        {
            return listGroup[groupPosition].Text;
        }

        public override long GetGroupId(int groupPosition)
        {
            return groupPosition;
        }

        public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
        {
            string textGroup = (string)GetGroup(groupPosition);
            var item = this.listGroup.Where(w => w.Text == textGroup).FirstOrDefault();

            var gh = RatingGroupHeaderFactory.Create(item.TestType);

            if (gh != null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(gh.LayoutId, null);
                gh.Render(convertView, item);
            }
            
            return convertView;
        }

        public static ExpandableListViewAdapter CreateFromData(Context context, List<SeriesRatingsData> seriesRatingsData)
        {
            List<RatingGroupHeader> group = new List<RatingGroupHeader>();
            Dictionary<RatingGroupHeader, List<Java.Lang.Object>> expandList = new Dictionary<RatingGroupHeader, List<Java.Lang.Object>>();
            
            var firstSeries = seriesRatingsData.First();

            FrontalRatingsSmallOverlap ratingForPrimarySmallOverlapFront = firstSeries.frontalRatingsSmallOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            var smallOverlapGh = new RatingGroupHeader { TestType = ETestTypes.SmallOverlap, QualifyingText = ratingForPrimarySmallOverlapFront?.qualifyingText, Text = "Small overlap front", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.overallRating) };
            group.Add(smallOverlapGh);

            List<Java.Lang.Object> smallOverlapFront = new List<Java.Lang.Object>
            {                
                new GampChildRowItem { Text = "Structure and safety cage", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.structureRating)},
                new GampChildRowItem { Text = "Injury measures - head/neck", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.headNeckRating)},
                new GampChildRowItem { Text = "Injury measures - chest", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.chestRating)},
                new GampChildRowItem { Text = "Injury measures - hip/thigh", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.femurPelvisRating)},
                new GampChildRowItem { Text = "Injury measures - lower leg/foot", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.footTibiaRating)},
                new GampChildRowItem { Text = "Restraints and dummy kinematics", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.kinematicsRating)},
            };

            FrontalRatingsModerateOverlap ratingForPrimaryModerateOverlapFront = firstSeries.frontalRatingsModerateOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            var frontalGh = new RatingGroupHeader { TestType = ETestTypes.Frontal, QualifyingText = ratingForPrimaryModerateOverlapFront?.qualifyingText, Text = "Moderate overlap front", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.overallRating) };
            group.Add(frontalGh);

            List<Java.Lang.Object> moderateOverlapFront = new List<Java.Lang.Object>
            {                
                new GampChildRowItem { Text = "Structure and safety cage", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.structureRating)},
                new GampChildRowItem { Text = "Injury measures - head/neck", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.headNeckRating)},
                new GampChildRowItem { Text = "Injury measures - chest", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.chestRating)},
                new GampChildRowItem { Text = "Injury measures - leg/foot left", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.leftLegRating)},
                new GampChildRowItem { Text = "Injury measures - leg/foot right", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.rightLegRating)},
                new GampChildRowItem { Text = "Restraints and dummy kinematics", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.kinematicsRating)},
            };

            SideRating ratingForPrimarySide = firstSeries.sideRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            var sideGh = new RatingGroupHeader { TestType = ETestTypes.Side, Text = "Side", QualifyingText = ratingForPrimarySide?.qualifyingText, ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.overallRating) };
            group.Add(sideGh);
            
            List<Java.Lang.Object> side = new List<Java.Lang.Object>
            {             
                new GampChildRowItem { Text = "Structure and safety cage", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.structureRating)},
                new GampChildRowItem { Text = "Driver injury measures - head/neck", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.driverHeadNeckRating)},
                new GampChildRowItem { Text = "Driver injury measures - torso", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.driverTorsoRating)},
                new GampChildRowItem { Text = "Driver injury measures - pelvis/leg", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.driverPelvisLegRating)},
                new GampChildRowItem { Text = "Driver injury measures - head protection", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.driverHeadProtectionRating)},
                new GampChildRowItem { Text = "Passenger injury measures - head/neck", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.passengerHeadNeckRating)},
                new GampChildRowItem { Text = "Passenger injury measures - torso", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.passengerTorsoRating)},
                new GampChildRowItem { Text = "Passenger injury measures - pelvis/leg", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.passengerPelvisLegRating)},
                new GampChildRowItem { Text = "Passenger injury measures - head protection", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.passengerHeadProtectionRating)},
            };

            RolloverRating ratingForPrimaryRoofStrength = firstSeries.rolloverRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            var roofGh = new RatingGroupHeader { TestType = ETestTypes.RoofCrush, Text = "Roof strength", QualifyingText = ratingForPrimaryRoofStrength?.qualifyingText, ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryRoofStrength?.overallRating) };
            group.Add(roofGh);
            List<Java.Lang.Object> roofStrength = new List<Java.Lang.Object>();

            if (ratingForPrimaryRoofStrength != null)
            {
                roofStrength = new List<Java.Lang.Object>
                {
                    new DataChildRowItem { Text = "Curb weight", Value = $"{ratingForPrimaryRoofStrength.weight} lbs" },
                    new DataChildRowItem { Text = "Peak force", Value = $"{ratingForPrimaryRoofStrength.force} lbs" },
                    new DataChildRowItem { Text = "Strength-to-weight ratio", Value = ratingForPrimaryRoofStrength?.ratio },
                    new DataChildRowItem { Text = "Tested vehicle", Value = ratingForPrimaryRoofStrength?.testSubject },
                };
            }
            
            RearRating ratingForPrimaryHeadRestraint = firstSeries.rearRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            var rearGh = new RatingGroupHeader { TestType = ETestTypes.Rear, QualifyingText = ratingForPrimaryHeadRestraint?.qualifyingText, Text = "Head restraints & seats", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadRestraint?.overallRating) };
            group.Add(rearGh);  
            
            List<Java.Lang.Object> headRestraint = new List<Java.Lang.Object>
            {             
                new GampChildRowItem { Text = "Dynamic rating", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadRestraint?.dynamicRating)},
                new GampChildRowItem { Text = "Seat/head restraint geometry", GampResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadRestraint?.geometryRating)},
            };

            FrontCrashPreventionRating ratingForPrimaryFrontCrashPrevention = firstSeries.frontCrashPreventionRatings.Where(w => w.isPrimary == true).FirstOrDefault();

            RatingGroupHeader aebGh = null;
            List<Java.Lang.Object> frontCrashPrevention = new List<Java.Lang.Object>();

            if (ratingForPrimaryFrontCrashPrevention != null)
            {
                aebGh = new RatingGroupHeader { Text = "Front crash prevention", ImageResourceId = UiHelper.GetResourceAeb(ratingForPrimaryFrontCrashPrevention.overallRating.totalPoints), RatingText = ratingForPrimaryFrontCrashPrevention?.overallRating.ratingText, QualifyingText = ratingForPrimaryFrontCrashPrevention?.qualifyingText, TestType = ETestTypes.CrashAvoidance };
                group.Add(aebGh);

                frontCrashPrevention = new List<Java.Lang.Object>
                {
                    new DataChildRowItem { Text = "System Details", Value = (ratingForPrimaryFrontCrashPrevention.autobrake.availability + " " + ratingForPrimaryFrontCrashPrevention.autobrake.systemName) },
                    new DataChildRowItem { Text = "Package name", Value = (ratingForPrimaryFrontCrashPrevention.forwardCollisionWarning.availability + " " + ratingForPrimaryFrontCrashPrevention.forwardCollisionWarning.packageName) },
                    new DataChildRowItem { Text = "Forward collision warning", Value = $"{ratingForPrimaryFrontCrashPrevention.forwardCollisionWarning.points} of 1 point" },
                    new DataChildRowItem { Text = "Low-speed autobrake", Value = $"{ratingForPrimaryFrontCrashPrevention.autobrake.lowSpeedPoints} of 2 points" },
                    new DataChildRowItem { Text = "High-speed autobrake", Value = $"{ratingForPrimaryFrontCrashPrevention.autobrake.highSpeedPoints} of 3 points" }
                };
            }

            HeadlightRating ratingForPrimaryHeadlights = firstSeries.headlightRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            var headlightsGh = new RatingGroupHeader { TestType = ETestTypes.Headlight, Text = "Headlights", QualifyingText = ratingForPrimaryHeadlights?.qualifyingText, ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadlights?.overallRating) };
            group.Add(headlightsGh);
            List<Java.Lang.Object> headlights = new List<Java.Lang.Object>();
            if (ratingForPrimaryHeadlights != null)
            {
                headlights = new List<Java.Lang.Object>
                {
                    //new DataChildRowItem { Text = "Trim Level(s)", Value = StringHelper.SmartAppend(", ",ratingForPrimaryHeadlights.trims.Select(s => $"{s.description}({s.optionalPackage})").ToArray())},
                    new DataChildRowItem { Text = "Low-beam headlight type", Value = ratingForPrimaryHeadlights.sourceLowBeamDescription },
                    new DataChildRowItem { Text = "High-beam headlight type", Value = ratingForPrimaryHeadlights.sourceHighBeamDescription },
                    new DataChildRowItem { Text = "Curve-adaptive?", Value = ratingForPrimaryHeadlights.curveAdaptive ? "yes" : "no" },
                    new DataChildRowItem { Text = "Automatically switches between low beams and high beams (high-beam assist)?", Value = ratingForPrimaryHeadlights.highBeamAssist ? "yes" : "no"}
                };
            }

            expandList.Add(smallOverlapGh, smallOverlapFront);
            expandList.Add(frontalGh, moderateOverlapFront);
            expandList.Add(sideGh, side);
            expandList.Add(roofGh, roofStrength);
            expandList.Add(rearGh, headRestraint);

            if (ratingForPrimaryFrontCrashPrevention != null)
            {
                expandList.Add(aebGh, frontCrashPrevention);                
            }

            if (ratingForPrimaryHeadlights != null)
                expandList.Add(headlightsGh, headlights);

            return new ExpandableListViewAdapter(context, group, expandList);
        }


        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return false;
        }
    }
}