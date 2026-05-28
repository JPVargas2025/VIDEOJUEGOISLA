# Documentation Rules and Standards

**Created at:** 2026-03-03

---

## Overview

This document defines the documentation standards for the Tiny Habits Tracker project. All documentation must follow these rules to ensure consistency, clarity, and ease of navigation for current and future developers.

---

## Core Documentation Principles

### 1. Centralization
- **Location**: All documentation files MUST be in the `/documentation` folder
- **Index**: Root `README.md` MUST link to all documentation files
- **No Duplication**: Only ONE file per topic/subject/implementation
- **Version Control**: Documentation is version-controlled alongside code

### 2. Accessibility
- **Language**: Clear, non-technical language where possible
- **Structure**: Logical, hierarchical organization
- **Search**: Descriptive filenames enable easy searching
- **Links**: Cross-references between related documents
- **New Developers**: Should be able to understand features quickly

### 3. Maintenance
- **Ownership**: Each document has a primary owner (documented at top)
- **Updates**: Modified documents must show "Updated at" timestamp
- **Review**: Documentation reviewed in PR process
- **Pruning**: Outdated documentation removed or archived

---

## File Naming Convention

### Format
```
[SUBJECT]_[ASPECT].md
```

### Examples
- `PHASES_AND_EXECUTION.md` - Phase planning and execution
- `DATABASE_SCHEMA.md` - Database structure
- `AUTHENTICATION_DESIGN.md` - Auth system design
- `GEMINI_API_INTEGRATION.md` - AI chatbot integration
- `STRIPE_INTEGRATION.md` - Payment system
- `GOOGLE_CALENDAR_INTEGRATION.md` - Calendar sync
- `NOTIFICATION_ARCHITECTURE.md` - Notification system
- `PHASE_1_IMPLEMENTATION.md` - Phase 1 implementation details
- `TEST_RESULTS_PHASE_1.md` - Phase 1 test results
- `SETUP_GUIDE.md` - Developer setup instructions
- `DEPLOYMENT_PROCEDURES.md` - Production deployment guide

### Naming Rules
- **Format**: SCREAMING_SNAKE_CASE with underscores
- **Descriptive**: Filename should be self-documenting
- **No Generic**: Avoid generic names like "README.md" (except root)
- **Subject-Specific**: Group related files by subject (PHASE_*, API_*, DATABASE_*, etc.)

---

## Document Structure

### Header Section (MANDATORY)

Every document must start with:

```markdown
# [Document Title]

**Created at:** YYYY-MM-DD HH:MM (e.g., 2026-03-03 14:30)

OR (if updated)

**Updated at:** YYYY-MM-DD HH:MM
**Original created:** YYYY-MM-DD

---
```

### Document Body (Standard Sections)

#### 1. Mission or Problem Statement
**What**: Brief description of what the document is about
**Why**: Why this exists and its importance to the project

```markdown
## Mission Statement

This document defines...
The problem it solves...
```

#### 2. What Was Done?
Describe the implementation, feature, or change

```markdown
## What Was Implemented

- Item 1
- Item 2
- Item 3

### Deliverables
- [ ] Deliverable 1
- [ ] Deliverable 2
```

#### 3. Why It Was Done?
Business and technical reasons

```markdown
## Why

### Business Reasons
- Customer requirement
- Competitive advantage
- Revenue generation

### Technical Reasons
- Performance improvement
- Code maintainability
- Scalability requirement
```

#### 4. How It Was Done?
Technical implementation details

```markdown
## How

### Architecture
[Describe architecture]

### Technology Choices
- Technology X: Reason for choice
- Technology Y: Reason for choice

### Implementation Steps
1. Step one
2. Step two
3. Step three
```

#### 5. Why Not Another Way?
Alternative approaches and why they were rejected

```markdown
## Why Not Alternative Approaches?

### Approach A
**Considered**: Yes
**Reason rejected**: [Explanation]

### Approach B
**Considered**: Yes
**Reason rejected**: [Explanation]
```

#### 6. Benefits, Advantages, Disadvantages & Risks

```markdown
## Benefits and Advantages
- Benefit 1: Description
- Benefit 2: Description

## Disadvantages
- Disadvantage 1: Mitigation
- Disadvantage 2: Mitigation

## Risks and Mitigations
| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Risk 1 | High/Med/Low | High/Med/Low | Action |
| Risk 2 | High/Med/Low | High/Med/Low | Action |
```

#### 7. Results
What was the outcome?

```markdown
## Results

### Metrics
- Metric 1: Value
- Metric 2: Value

### Validation
- How was success measured?
- Were goals met?
```

#### 8. Lessons Learned
What should the team remember?

```markdown
## Lessons Learned

### What Went Well
- Item 1
- Item 2

### What Could Be Better
- Item 1
- Item 2

### Key Takeaways
- Takeaway 1
- Takeaway 2
```

#### 9. Recommendations
Guidance for future work

```markdown
## Recommendations

### For Next Phase
- Action 1
- Action 2

### For Similar Implementations
- Recommendation 1
- Recommendation 2

### Preventive Measures
- Measure 1
- Measure 2
```

---

## Markdown Best Practices

### Formatting

#### Headings
```markdown
# Level 1 - Document Title Only
## Level 2 - Main Sections
### Level 3 - Subsections
#### Level 4 - Details
```

#### Lists
```markdown
### Unordered Lists
- Item 1
- Item 2
  - Sub-item 2.1
  - Sub-item 2.2

### Ordered Lists
1. First item
2. Second item
   1. Sub-item 2.1
   2. Sub-item 2.2

### Checkbox Lists (for checklists)
- [x] Completed task
- [ ] Pending task
```

#### Code Blocks
```markdown
### Inline Code
Use `code` for inline references

### Code Blocks
Use triple backticks with language specification:

\`\`\`typescript
// TypeScript example
const greeting: string = "Hello";
\`\`\`

\`\`\`sql
-- SQL example
SELECT * FROM users WHERE active = true;
\`\`\`

\`\`\`json
{
  "key": "value"
}
\`\`\`
```

#### Tables
```markdown
| Header 1 | Header 2 | Header 3 |
|----------|----------|----------|
| Cell 1   | Cell 2   | Cell 3   |
| Cell 4   | Cell 5   | Cell 6   |
```

#### Links and References
```markdown
### Internal Links
[Link text](/path/to/document.md)
[PHASES_AND_EXECUTION](/documentation/PHASES_AND_EXECUTION.md)

### External Links
[Link text](https://external-url.com)

### Code References
Reference code locations: `file_path:line_number`
Example: The user creation logic is in `src/services/auth.ts:45`
```

#### Emphasis
```markdown
**Bold text** for important terms
*Italic text* for emphasis
~~Strikethrough~~ for deprecated items
```

#### Blockquotes
```markdown
> This is a blockquote
> Can span multiple lines
> Use for important notes
```

### Line Spacing and Readability
- Use 1-2 blank lines between sections
- Use single blank line between list items if detailed
- Keep paragraphs under 150 words
- Use bullet points for lists over 3 items

### Avoid
- Excessive emoji (use only if explicitly requested)
- Mixed formatting (don't over-style)
- Very long code blocks (reference file instead)
- Unclear abbreviations
- Outdated information without update timestamp

---

## Phase Documentation Pattern

### Phase Implementation Files
**Filename**: `PHASE_[N]_IMPLEMENTATION.md`

```markdown
# Phase [N] Implementation Summary

**Created at:** YYYY-MM-DD HH:MM

## Mission Statement
[Copy from PHASES_AND_EXECUTION.md]

## What Was Implemented
- Feature 1
- Feature 2
- Feature 3

### Deliverables Completed
- [x] Deliverable 1
- [x] Deliverable 2
- [x] Deliverable 3

## Architecture and Decisions
### Architecture Diagram
[ASCII diagram or description]

### Technology Choices
- Choice 1: Reason
- Choice 2: Reason

## Known Issues and Resolutions
| Issue | Status | Resolution |
|-------|--------|-----------|
| Issue 1 | Resolved | Description |

## What Went Well
- Item 1
- Item 2

## Improvements for Next Phase
- Item 1
- Item 2

## References
- [PHASES_AND_EXECUTION](/documentation/PHASES_AND_EXECUTION.md)
- [Related Doc](/documentation/related_doc.md)
```

### Test Results Files
**Filename**: `TEST_RESULTS_PHASE_[N].md`

```markdown
# Phase [N] Test Results

**Created at:** YYYY-MM-DD HH:MM

## Test Execution Summary
- **Date**: YYYY-MM-DD
- **Duration**: X hours
- **Environment**: Development/Staging
- **Tester(s)**: [Names]

## KPI Gates Achievement
| KPI | Target | Achieved | Status |
|-----|--------|----------|--------|
| KPI 1 | 95% | 97% | ✅ PASS |
| KPI 2 | 100% | 98% | ⚠️ INVESTIGATE |

## Test Results by Category

### Unit Tests
- Total tests: X
- Passed: X
- Failed: X
- Skipped: X
- Coverage: X%

### Integration Tests
[Similar format]

### End-to-End Tests
[Similar format]

### Performance Tests
- Metric 1: Value (target: Y)
- Metric 2: Value (target: Y)

## Issues Found

### Critical Issues
| ID | Description | Status | Resolution |
|---|---|---|---|
| CRI-001 | Issue description | Fixed | Description |

### High Priority Issues
[Similar table]

### Medium Priority Issues
[Similar table]

### Low Priority Issues
[Similar table]

## Recommendations
1. Recommendation 1
2. Recommendation 2

## Sign-Off
- [ ] Tech Lead approved
- [ ] Product Manager approved
- [ ] Ready for Phase [N+1]
```

---

## Documentation Ownership

### Document Owner Responsibility
Each document should indicate its owner:

```markdown
# Document Title

**Created at:** YYYY-MM-DD
**Owner**: [Developer Name]
**Last Reviewed**: YYYY-MM-DD
```

### Owner Responsibilities
- Keep documentation current
- Review updates before merging PRs
- Respond to documentation questions
- Archive or delete obsolete documentation

---

## Documentation in Pull Requests

### Checklist for PRs
- [ ] Code changes documented (inline comments only where logic isn't obvious)
- [ ] New features documented in relevant doc file
- [ ] Architecture changes documented
- [ ] API changes documented
- [ ] Database changes documented
- [ ] Test results added/updated
- [ ] Timestamp updated in affected docs

### PR Description Template
```markdown
## Description
[What does this PR do?]

## Related Documentation
- [DOCUMENT_NAME](/documentation/document_name.md)

## Changes
- What changed
- What changed

## Testing
- How was this tested?
- Test results

## Documentation Updated
- [x] DOCUMENT_NAME.md
- [ ] No documentation updates needed
```

---

## Documentation Review Checklist

Before merging documentation changes:

- [ ] Filename follows naming convention
- [ ] All required sections present
- [ ] Timestamp included (Created/Updated at)
- [ ] No spelling/grammar errors
- [ ] Code examples are correct
- [ ] Links are working
- [ ] Tables are properly formatted
- [ ] Markdown renders correctly
- [ ] No sensitive information (API keys, secrets)
- [ ] Related docs are linked
- [ ] Tech owner reviewed

---

## Special Documentation Types

### API Documentation
**Location**: `/documentation/API_*.md`

```markdown
# API Documentation

## Endpoint

### GET /api/endpoint

**Description**: What this endpoint does

**Authentication**: Required/Not required

**Request**
```json
{
  "param1": "value"
}
```

**Response** (200 OK)
```json
{
  "data": [],
  "status": "success"
}
```

**Error Responses**
- 400 Bad Request
- 401 Unauthorized
- 500 Internal Server Error
```

### Database Schema Documentation
**Location**: `/documentation/DATABASE_*.md`

```markdown
# Table Name

## Purpose
What is this table for?

## Schema
| Column | Type | Constraints | Description |
|--------|------|-------------|------------|
| id | uuid | PK | Primary identifier |

## Relationships
- Foreign key to table X
- Foreign key to table Y

## Indexes
- Index on column A
- Index on column B

## Row-Level Security
- Policy 1
- Policy 2

## Example Queries
\`\`\`sql
SELECT * FROM table WHERE condition
\`\`\`
```

### Architecture Documentation
**Location**: `/documentation/ARCHITECTURE_*.md`

```markdown
# [System] Architecture

## Overview
High-level description

## Components
### Component 1
[Description]

### Component 2
[Description]

## Data Flow
[Diagram description or ASCII art]

## Integration Points
- External service A
- External service B

## Scalability Considerations
- Horizontal scaling
- Caching strategy
- Database optimization
```

---

## Linking and Navigation

### Root README.md Structure
```markdown
# Tiny Habits Tracker

## Documentation Index

### Getting Started
- [Setup Guide](/documentation/SETUP_GUIDE.md)
- [Architecture Overview](/documentation/ARCHITECTURE_OVERVIEW.md)

### Development
- [Phase Planning](/documentation/PHASES_AND_EXECUTION.md)
- [Database Schema](/documentation/DATABASE_SCHEMA.md)
- [API Documentation](/documentation/API_DOCUMENTATION.md)

### Phase Implementations
- [Phase 1 Implementation](/documentation/PHASE_1_IMPLEMENTATION.md)
- [Phase 1 Test Results](/documentation/TEST_RESULTS_PHASE_1.md)

### Integration Guides
- [Gemini API Integration](/documentation/GEMINI_API_INTEGRATION.md)
- [Stripe Integration](/documentation/STRIPE_INTEGRATION.md)

### Operations
- [Deployment Guide](/documentation/DEPLOYMENT_PROCEDURES.md)
- [Production Runbook](/documentation/PRODUCTION_RUNBOOK.md)
```

### Cross-Document Links
Always link related documentation:
- Architecture docs link to implementation docs
- Implementation docs link to test results
- Test results link to issues/lessons learned
- Phase completion docs link to next phase

---

## Documentation Lifecycle

### New Documentation
1. Create file with descriptive name
2. Add header with "Created at" timestamp
3. Add owner information
4. Follow standard structure
5. Link from README.md
6. Submit PR for review

### Updating Documentation
1. Change header to "Updated at" timestamp
2. Keep "Original created" timestamp
3. Summarize changes in top section
4. Submit PR with change explanation

### Archiving Documentation
1. Move to `/documentation/archive/` folder
2. Keep in version control (history preserved)
3. Remove from README.md
4. Note archival reason in file header

### Deleting Documentation
- Delete requires user approval and only if completely obsolete
- Prefer archiving for historical reference
- Ensure information not needed elsewhere

---

## Tools and Technologies

### Markdown Editors
- VS Code with Markdown Preview
- GitHub's built-in editor
- Any text editor with Markdown support

### Validation
- Run Markdown linter before commit
- Check links are working
- Verify code examples are correct

### Version Control
- Documentation committed alongside code
- Commit messages reference related code changes
- PRs review documentation changes

---

## Example Documentation: Complete File

```markdown
# Habit Streak Calculation Algorithm

**Created at:** 2026-03-03 14:30

---

## Mission Statement

This document explains how the habit tracking system calculates and maintains streaks for user habits. Streaks are a critical motivational element in the Tiny Habits methodology, providing immediate feedback on consistency.

## What Was Implemented

The streak calculation system includes:
- Current streak tracking
- All-time longest streak tracking
- Streak reset logic
- Catch-up day handling (up to 7 days)

### Components
- `calculateStreak()` function in `src/services/habits.ts`
- `StreakTracker` database table
- Nightly batch job to recalculate streaks

## Why It Was Done

### Business Reasons
- Streaks are proven habit-building motivators
- Users want to see consistent progress
- Competitive element improves engagement

### Technical Reasons
- Efficient database querying
- Accurate historical tracking
- Handles edge cases (time zones, missed days)

## How It Was Done

### Algorithm
1. Query all daily tracking entries for habit
2. Sort by date ascending
3. Identify consecutive days of completion
4. Calculate current and longest streaks
5. Store in StreakTracker table

### Code Example
\`\`\`typescript
function calculateStreak(habitId: string): Streak {
  // Query tracking entries
  const entries = await getTrackingEntries(habitId);

  // Sort and identify streaks
  let currentStreak = 0;
  let longestStreak = 0;
  // ... calculation logic

  return { currentStreak, longestStreak };
}
\`\`\`

## Why Not Alternative Approaches?

### Approach A: Real-Time Calculation
**Considered**: Yes
**Reason rejected**: Slow on habit records with large history

### Approach B: Batch Calculation Only
**Considered**: Yes
**Reason rejected**: Up to 24-hour delay in UI update

**Chosen Solution**: Real-time with nightly verification batch job

## Benefits and Advantages
- Immediate feedback to users
- Motivational element in UI
- Accurate across all time zones

## Disadvantages
- Additional database writes
- Batch job overhead

## Risks and Mitigations
| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Streak calculation error | Low | High | Nightly verification batch |
| Performance degradation | Medium | Medium | Database indexing |
| Time zone confusion | Low | Medium | UTC storage with conversion |

## Results

### Metrics
- Streak calculation accuracy: 99.9%
- Query response time: <50ms
- User engagement increase: 12%

### Validation
- Automated tests verify 30-day periods
- Manual QA on various time zones
- User feedback positive

## Lessons Learned

### What Went Well
- Batch verification approach reliable
- Database indexes effective
- Users love streak feature

### What Could Be Better
- Initial calculation took longer than expected
- Should cache longest streak separately

## Recommendations

### For Next Phase
- Add streak milestones (10, 30, 100 day achievements)
- Implement streak sharing feature
- Add streak comparison with other users

### For Similar Features
- Always include nightly verification for calculated fields
- Consider user psychology when designing gamification

---

## References
- [PHASES_AND_EXECUTION](/documentation/PHASES_AND_EXECUTION.md) - Phase 2 requirements
- [DATABASE_SCHEMA](/documentation/DATABASE_SCHEMA.md) - StreakTracker table
- `src/services/habits.ts:120` - Implementation code
```

---

## Conclusion

Following these documentation rules ensures:
- **Consistency**: All developers follow same pattern
- **Clarity**: New team members understand quickly
- **Maintainability**: Documentation stays current
- **Professionalism**: Comprehensive, well-organized documentation

Documentation is code. Treat it with same care as implementation.
