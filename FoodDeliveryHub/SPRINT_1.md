# Sprint 1 — Fitness domain

**Branch:** `feature/fitness-sprint1`  
**Base:** `main`  
**Method:** Agile / Scrum

## Suggested Trello cards (copy into your team's board)

| Card | Owner | Acceptance criteria |
|---|---|---|
| Entities and enums | Assign team member | 10 entities, primary/foreign keys, WorkoutExercise N:M, private setters |
| Generic repository | Assign team member | IRepository<T> and CRUD implementation, duplicate/not-found behavior |
| Business logic | Assign team member | Mifflin-St Jeor BMR/TDEE and ±15% goal, Premium access, validation |
| Testing and review | Assign reviewer | Smoke checks run successfully; code reviewed and approved |

Trello workflow: **To Do → In Progress → Testing / Code Review → Done**.
Daily stand-up: what was completed, what is next, and any blockers.
Prefer separate branches per teammate (`feature/entity-layer`,
`feature/repository-layer`, `feature/business-layer`) and PRs against this
integration branch when multiple people edit concurrently.

## Review checklist
- [ ] All C# projects build in Visual Studio.
- [ ] BMR/TDEE examples match expected calculations.
- [ ] Free workouts are accessible without subscriptions.
- [ ] Premium requires an active subscription.
- [ ] CRUD methods and missing-record behavior work.
- [ ] Invalid age, height, weight, email and promo are rejected.
- [ ] Peer review completed; Trello cards updated.
- [ ] Demo and sprint retrospective completed.
- [ ] Each team member posts GitHub and Trello links in Classroom.

GitHub integration branch:
https://github.com/YoanDemerdzhiev/FoodHub/tree/feature/fitness-sprint1

Note: Trello cards, peer review, and Classroom submissions must be completed
by the team; no Trello board was supplied.
