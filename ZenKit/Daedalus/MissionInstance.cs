using System;

namespace ZenKit.Daedalus
{
	public class MissionInstance  : DaedalusInstance
	{
		public MissionInstance(UIntPtr handle) : base(handle)
		{
		}	

		public string Name => Native.ZkMissionInstance_getName(Handle).MarshalAsString() ?? string.Empty;
		public string Description => Native.ZkMissionInstance_getDescription(Handle).MarshalAsString() ?? string.Empty;
		public int Duration => Native.ZkMissionInstance_getDuration(Handle);
		public int Important => Native.ZkMissionInstance_getImportant(Handle);
		public int OfferConditionsFn => Native.ZkMissionInstance_getOfferConditions(Handle);
		public int OfferFn => Native.ZkMissionInstance_getOffer(Handle);
		public int SuccessConditionsFn => Native.ZkMissionInstance_getSuccessConditions(Handle);
		public int SuccessFn => Native.ZkMissionInstance_getSuccess(Handle);
		public int FailureConditionsFn => Native.ZkMissionInstance_getFailureConditions(Handle);
		public int FailureFn => Native.ZkMissionInstance_getFailure(Handle);
		public int ObsoleteConditionsFn => Native.ZkMissionInstance_getObsoleteConditions(Handle);
		public int ObsoleteFn => Native.ZkMissionInstance_getObsolete(Handle);
		public int RunningFn => Native.ZkMissionInstance_getRunning(Handle);
	}
}
