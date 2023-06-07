import { format } from 'date-fns';
import { observer } from 'mobx-react-lite';
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import {Button, Header, Item, Segment, Image, Label, Dropdown} from 'semantic-ui-react';
import { Activity } from "../../../app/models/activity";
import { useStore } from '../../../app/stores/store';
import MyOwnSelect from "../../../app/common/form/MyOwnSelect";

const activityImageStyle = {
    filter: 'brightness(30%)'
};

const activityImageTextStyle = {
    position: 'absolute',
    bottom: '5%',
    left: '5%',
    width: '100%',
    height: 'auto',
    color: 'white'
};

interface Props {
    activity: Activity
}

export default observer(function ActivityDetailedHeader({ activity }: Props) {
    const { activityStore: { updateAttendance, loading, cancelActivityToggle } } = useStore();
    const [selectedOption, setSelectedOption] = useState<string | null>(null);

    const handleSubmit = (values: Props) => {
        // Handle form submission
        console.log(values);
    };

    const handleVote = () => {
        // Handle the vote action here
        // You can use the selectedOption value to perform the vote action
        console.log("Voted for:", selectedOption);
    };

    return (
        <Segment.Group>
            <Segment basic attached='top' style={{ padding: '0' }}>
                {activity.isCancelled &&
                    <Label style={{ position: 'absolute', zIndex: 1000, left: -14, top: 20 }}
                           ribbon color='red' content='Cancelled' />}
                <Image src={`/assets/categoryImages/${activity.category}.jpg`} fluid style={activityImageStyle} />
                <Segment style={activityImageTextStyle} basic>
                    <Item.Group>
                        <Item>
                            <Item.Content>
                                <Header
                                    size='huge'
                                    content={activity.title}
                                    style={{ color: 'white' }}
                                />
                                <p>{format(activity.closeDate!, 'dd MMM yyyy')}</p>
                                <p>
                                    Created by <strong><Link to={`/profiles/${activity.hostUsername}`}>{activity.hostUsername}</Link></strong>
                                </p>
                            </Item.Content>
                        </Item>
                    </Item.Group>
                </Segment>
            </Segment>
            <Segment clearing attached='bottom'>
                {activity.isHost ? (
                    <>
                        <Button
                            color={activity.isCancelled ? 'green' : 'red'}
                            floated='left'
                            basic
                            content={activity.isCancelled ? 'Re-activate Poll' : 'Cancel Poll'}
                            onClick={cancelActivityToggle}
                            loading={loading}
                        />
                        <Button
                            as={Link}
                            to={`/manage/${activity.id}`}
                            color='orange'
                            floated='right'
                            disabled={activity.isCancelled}
                        >
                            Manage Poll
                        </Button>
                    </>

                ) : activity.isGoing ? (
                    <Button onClick={updateAttendance}
                            loading={loading}>Cancel attendance</Button>
                ) : (
                    <>
                        <MyOwnSelect
                            options={activity.choices}
                            placeholder="Select an option"
                            name="choice"
                        />
                        <Button
                            onClick={handleVote}
                            disabled={!selectedOption}
                            loading={loading}
                            primary
                        >
                            Vote
                        </Button>
                    </>
                )}
            </Segment>
        </Segment.Group>
    )
});
