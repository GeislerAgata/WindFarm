export interface Sensor {
    _id: string,
    timestamp: Date,
    sensorId: number,
    sensorType: string,
    value: number,
    unit: string
}