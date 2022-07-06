type Props = {
    size: number;
}

export default function VerticalSpacing({ size }: Props) {
    return (
        <div style={{ height: `${size}px` }}></div>
    );
}