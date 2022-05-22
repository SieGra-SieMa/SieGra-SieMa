type VerticalSpacingProps = {
    size: number;
}

export default function VerticalSpacing({ size }: VerticalSpacingProps) {
    return (
        <div style={{ height: `${size}px` }}></div>
    );
}