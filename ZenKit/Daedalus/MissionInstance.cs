using System;

namespace ZenKit.Daedalus
{
	public class MissionInstance : DaedalusInstance
	{
		public MissionInstance(UIntPtr handle) : base(handle)
		{
		}

		public string Name
		{
			get => Native.ZkMissionInstance_getName(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMissionInstance_setName(Handle, value);
		}

		public string Description
		{
			get => Native.ZkMissionInstance_getDescription(Handle).MarshalAsString() ?? string.Empty;
			set => Native.ZkMissionInstance_setDescription(Handle, value);
		}

		public int Duration
		{
			get => Native.ZkMissionInstance_getDuration(Handle);
			set => Native.ZkMissionInstance_setDuration(Handle, value);
		}

		public int Important
		{
			get => Native.ZkMissionInstance_getImportant(Handle);
			set => Native.ZkMissionInstance_setImportant(Handle, value);
		}

		public int OfferConditionsFn
		{
			get => Native.ZkMissionInstance_getOfferConditions(Handle);
			set => Native.ZkMissionInstance_setOfferConditions(Handle, value);
		}

		public int OfferFn
		{
			get => Native.ZkMissionInstance_getOffer(Handle);
			set => Native.ZkMissionInstance_setOffer(Handle, value);
		}

		public int SuccessConditionsFn
		{
			get => Native.ZkMissionInstance_getSuccessConditions(Handle);
			set => Native.ZkMissionInstance_setSuccessConditions(Handle, value);
		}

		public int SuccessFn
		{
			get => Native.ZkMissionInstance_getSuccess(Handle);
			set => Native.ZkMissionInstance_setSuccess(Handle, value);
		}

		public int FailureConditionsFn
		{
			get => Native.ZkMissionInstance_getFailureConditions(Handle);
			set => Native.ZkMissionInstance_setFailureConditions(Handle, value);
		}

		public int FailureFn
		{
			get => Native.ZkMissionInstance_getFailure(Handle);
			set => Native.ZkMissionInstance_setFailure(Handle, value);
		}

		public int ObsoleteConditionsFn
		{
			get => Native.ZkMissionInstance_getObsoleteConditions(Handle);
			set => Native.ZkMissionInstance_setObsoleteConditions(Handle, value);
		}

		public int ObsoleteFn
		{
			get => Native.ZkMissionInstance_getObsolete(Handle);
			set => Native.ZkMissionInstance_setObsolete(Handle, value);
		}

		public int RunningFn
		{
			get => Native.ZkMissionInstance_getRunning(Handle);
			set => Native.ZkMissionInstance_setRunning(Handle, value);
		}
	}
}