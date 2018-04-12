# circle-of-trust-csharp
Circle of Trust clone in C#

Uses Domain-Driven Design.
Uses Hexagonal Architecture.
Uses CQRS.
Uses Event Sourcing.

Access with JSON RPC 2.0 over HTTP or WebSockets.

## Commands
```
RegisterPlayer
InitiateCircle
JoinCircle
BetrayCircle
```

## Queries
```
ReadPlayerStatsByPlayerId
ReadCircleStatsByCircleId
ReadCircleStatsByPlayerId
ReadAllPlayerStats
ReadAllCircleStats
ReadCircleLeaderboard
```

## Work that Needs to be Done
- Player Authentication
- Player Verification
- Event Store
- IDbConnection Lifetime Management
- Deployment Strategy
