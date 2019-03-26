export interface LastSeenItem {
    // Data section
    id?: string;
    modified?: Date;

    // Status section
    season?: number;
    episode?: number;
    visitUrl?: string;
    unfinished?: boolean;
    hours?: number;
    minutes?: number;
    seconds?: number;
    notes?: string;
    moveToTop?: boolean;

    // Config section
    name?: string;
    imageUrl?: string;

    // Tracking section
    trackingUrl?: string;
    episodesBehind?: number;
}
