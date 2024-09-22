using CleanBase.Core.Services.Jobs;
using Core.Constants;
using Core.Services;
using Serilog;

namespace Infrastructure.Jobs
{
    public class JobRegisterService : IProcessingJobConsumer
    {
        private static readonly TimeZoneInfo DefaultTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
        private readonly IBackgroundJob _backgroundJob;
        private string _cronExpression;
        private TimeZoneInfo _timeZone;
        private TimeSpan? _startTime;
        private TimeSpan? _endTime;

        public JobRegisterService(IBackgroundJob backgroundJob)
        {
            _backgroundJob = backgroundJob ?? throw new ArgumentNullException(nameof(backgroundJob));
            _timeZone = DefaultTimeZone;
        }

        public bool IsRunning { get; private set; }

        public Task HandleError(Exception exception, CancellationToken cancellationToken)
        {
            Console.Error.WriteLine($"Error occurred: {exception.Message}");
            return Task.CompletedTask;
        }

        public void SetSchedule(string cronExpression, TimeZoneInfo timeZone = null)
        {
            if (string.IsNullOrWhiteSpace(cronExpression))
                throw new ArgumentException("Cron expression cannot be null or whitespace.", nameof(cronExpression));

            _cronExpression = cronExpression;
            _timeZone = timeZone ?? DefaultTimeZone;
        }

        public void SetTimeRange(TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime <= startTime)
                throw new ArgumentException("End time must be greater than start time.");

            _startTime = startTime;
            _endTime = endTime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Log.Information("HELLLLO");

            //if (string.IsNullOrEmpty(_cronExpression))
            //	return Task.CompletedTask;

            //if (_startTime.HasValue && _endTime.HasValue)
            //{
            //	var currentTime = DateTime.Now.TimeOfDay;

            //	if (currentTime < _startTime.Value || currentTime > _endTime.Value)
            //	{
            //		return Task.CompletedTask;
            //	}
            //}
            //_backgroundJob.Recurring<IPetService>(JobNames.UPDATE_AGE, sv => sv.TriggerUpdateUserAge(), "* * * * *", _timeZone);

            //_backgroundJob.Recurring<IUserService>(JobNames.UPDATE_AGE,
            //	sv => sv.TriggerUpdateUserAge(), _cronExpression, _timeZone);

            Log.Information("SUCCESS REGISTER");
            IsRunning = true;

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _backgroundJob.RemoveJob(JobNames.UPDATE_AGE);

            IsRunning = false;

            return Task.CompletedTask;
        }
    }
}
