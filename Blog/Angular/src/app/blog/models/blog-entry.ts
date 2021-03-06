export interface BlogEntry {
    blogEntryId: number;
    title: string;
    body: string;
    email: string;
    userId: number;
    name: string;
    created: string;
    commentCount: number;
    urlFriendlyId: string;
    commentsDisabled: boolean;
}
