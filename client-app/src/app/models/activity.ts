import { Profile } from "./profile";
export class ActivityChoice {
    constructor(id: string, title: string, pollId: string) {
        this._id = id;
        this._title = title;
        this._pollId = pollId;
    }

    get pollId(): string {
        return this._pollId;
    }

    set pollId(value: string) {
        this._pollId = value;
    }
    get title(): string {
        return this._title;
    }

    set title(value: string) {
        this._title = value;
    }
    get id(): string {
        return this._id;
    }

    set id(value: string) {
        this._id = value;
    }
    public _id: string;
    private _title: string;
    private _pollId: string;
}
export interface Activity {
    id: string;
    title: string;
    description: string;
    category: string;
    createDate: Date | null;
    //array of choices
    choices: ActivityChoice[];
    closeDate: Date;
    hostUsername?: string;
    isCancelled?: boolean;
    isGoing?: boolean;
    isHost?: boolean
    voters: Profile[]
    host?: Profile;
}

export class ActivityFormValues
  {
    id?: string = undefined;
    title: string = '';
    category: string = '';
    description: string = '';
    choices: ActivityChoice[] = [];
    closeDate: Date | null = null;

	  constructor(activity?: ActivityFormValues) {
      if (activity) {
        this.id = activity.id;
        this.title = activity.title;
        this.category = activity.category;
        this.description = activity.description;
        this.choices = activity.choices;
        this.closeDate = activity.closeDate;
      }
    }

  }

  export class Activity implements Activity {
    constructor(init?: ActivityFormValues) {
      Object.assign(this, init);
    }
  }
