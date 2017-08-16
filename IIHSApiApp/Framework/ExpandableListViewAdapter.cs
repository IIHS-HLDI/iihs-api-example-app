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
        private Dictionary<RatingGroupHeader, List<string>> lstChild;

        public ExpandableListViewAdapter(Context context, List<RatingGroupHeader> listGroup, Dictionary<RatingGroupHeader, List<string>> lstChild)
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
            var result = new List<string>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result[childPosition];
        }

        public override long GetChildId(int groupPosition, int childPosition)
        {
            return childPosition;
        }

        public override int GetChildrenCount(int groupPosition)
        {
            var result = new List<string>();
            lstChild.TryGetValue(listGroup[groupPosition], out result);
            return result.Count;
        }

        public override View GetChildView(int groupPosition, int childPosition, bool isLastChild, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                LayoutInflater inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
                convertView = inflater.Inflate(Resource.Layout.item_layout, null);
            }
            TextView textViewItem = convertView.FindViewById<TextView>(Resource.Id.item);
            string content = (string)GetChild(groupPosition, childPosition);
            textViewItem.Text = content;
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
                gh.RenderView(convertView, item);
            }
            
            return convertView;
        }

        public static ExpandableListViewAdapter CreateFromData(Context context, List<SeriesRatingsData> seriesRatingsData)
        {
            List<RatingGroupHeader> group = new List<RatingGroupHeader>();
            Dictionary<RatingGroupHeader, List<string>> expandList = new Dictionary<RatingGroupHeader, List<string>>();

            List<string> smallOverlapFront = new List<string>
            {
                "Overall evaluation",
                "Structure and safety cage",
                "Injury measures",
                "Restraints and dummy kinematics"
            };
            List<string> moderateOverlapFront = new List<string>
            {
                "Overall evaluation",
                "Structure and safety cage",
                "Injury measures",
                "Restraints and dummy kinematics"
            };
            List<string> side = new List<string>
            {
                "Overall evaluation",
                "Structure and safety cage",
                "Injury measures",
                "Restraints and dummy kinematics"
            };
            List<string> roofStrength = new List<string>
            {
                "Overall evaluation",
                "Curb weight",
                "Peak force",
                "Strength-to-weight ratio",
                "Tested vehicle"
            };
            List<string> headRestraint = new List<string>
            {
                "Overall evaluation",
                "Dynamic rating",
                "Seat/head restraint geometry"
            };
            List<string> frontCrashPrevention = new List<string>
            {
                "Overall evaluation",
                "Forward collision warning",
                "Low-speed autobrake",
                "High-speed autobrake"
            };
            List<string> headlights = new List<string>
            {
                "Trim level(s)",
                "Low-beam headlight type",
                "High-beam headlight type",
                "Curve-adaptive?",
                "Automatically switches between low beams and high beams (high-beam assist)?",
                "Overall evaluation",
                "Distance at which headlights provide at least 5 lux illumination:",
                "Low beams",
                "High beams"
            };

            var firstSeries = seriesRatingsData.First();

            FrontalRatingsSmallOverlap ratingForPrimarySmallOverlapFront = firstSeries.frontalRatingsSmallOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { TestType = ETestTypes.SmallOverlap, Text = "Small overlap front:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimarySmallOverlapFront?.overallRating) });

            FrontalRatingsModerateOverlap ratingForPrimaryModerateOverlapFront = firstSeries.frontalRatingsModerateOverlap.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { TestType = ETestTypes.Frontal, Text = "Moderate overlap front:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryModerateOverlapFront?.overallRating) });

            SideRating ratingForPrimarySide = firstSeries.sideRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { TestType = ETestTypes.Side, Text = "Side:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimarySide?.overallRating) });

            RolloverRating ratingForPrimaryRoofStrength = firstSeries.rolloverRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { TestType = ETestTypes.RoofCrush, Text = "Roof strength:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryRoofStrength?.overallRating) });

            RearRating ratingForPrimaryHeadRestraint = firstSeries.rearRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { TestType = ETestTypes.Rear, Text = "Head restraints & seats:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadRestraint?.overallRating) });

            FrontCrashPreventionRating ratingForPrimaryFrontCrashPrevention = firstSeries.frontCrashPreventionRatings.Where(w => w.isPrimary == true).FirstOrDefault();

            if (ratingForPrimaryFrontCrashPrevention != null)
            {
                group.Add(new RatingGroupHeader { Text = "Front crash prevention:", ImageResourceId = UiHelper.GetResourceAeb(ratingForPrimaryFrontCrashPrevention.overallRating.totalPoints), RatingText = ratingForPrimaryFrontCrashPrevention?.overallRating.ratingText, RatingSubtext = ratingForPrimaryFrontCrashPrevention?.qualifyingText, TestType = ETestTypes.CrashAvoidance });
            }
            //  Yes this^ is returning the nr gamp every time ¯\_(ツ)_/¯ I'll fix it later

            HeadlightRating ratingForPrimaryHeadlights = firstSeries.headlightRatings.Where(w => w.isPrimary == true).FirstOrDefault();
            group.Add(new RatingGroupHeader { TestType = ETestTypes.Headlight, Text = "Headlights:", ImageResourceId = UiHelper.GetResourceGamp(ratingForPrimaryHeadlights?.overallRating) });

            expandList.Add(group[0], smallOverlapFront);
            expandList.Add(group[1], moderateOverlapFront);
            expandList.Add(group[2], side);
            expandList.Add(group[3], roofStrength);
            expandList.Add(group[4], headRestraint);

            if (frontCrashPrevention != null)
            {
                expandList.Add(group[5], frontCrashPrevention);
                expandList.Add(group[6], headlights);
            }
            else
            {
                expandList.Add(group[5], headlights);
            }

            return new ExpandableListViewAdapter(context, group, expandList);
        }


        public override bool IsChildSelectable(int groupPosition, int childPosition)
        {
            return true;
        }
    }
}