export class PublicMetrics {
    retweet_count: number;
    reply_count: number;
    like_count: number;
    quote_count: number;
}

export class Domain {
    id: string;
    name: string;
    description: string;
}

export class Entity {
    id: string;
    name: string;
    description: string;
}

export class ContextAnnotation {
    domain: Domain;
    entity: Entity;
}

export class Mention {
    start: number;
    end: number;
    username: string;
    id: string;
}

export class Hashtag {
    start: number;
    end: number;
    tag: string;
}

export class Annotation {
    start: number;
    end: number;
    probability: number;
    type: string;
    normalized_text: string;
}

export class Url {
    start: number;
    end: number;
    url: string;
    expanded_url: string;
    display_url: string;
    media_key: string;
    status?: number;
    unwound_url: string;
}

export class Entities {
    mentions: Mention[];
    hashtags: Hashtag[];
    annotations: Annotation[];
    urls: Url[];
}

export class Datum {
    public_metrics: PublicMetrics;
    author_id: string;
    created_at: Date;
    context_annotations: ContextAnnotation[];
    possibly_sensitive: boolean;
    text: string;
    entities: Entities;
    id: string;
}

export class Error {
    value: string;
    detail: string;
    title: string;
    resource_type: string;
    parameter: string;
    resource_id: string;
    type: string;
    section: string;
}

export class RootObject {
    data: Datum[];
    errors: Error[];
}
