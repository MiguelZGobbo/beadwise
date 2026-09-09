namespace BeadWise.Core.Sessions;

public enum SessionState
{
    Created,
    Planned,
    Approved,
    Applying,
    Verifying,
    Completed,
    Failed,
    Recovering,
    Recovered
}
